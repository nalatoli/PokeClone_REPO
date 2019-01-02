/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    /* Public Instance */
    public static PlayerMovement instance;

    /* Adjustable Parameter */
    public bool godMode;
    public bool isControllable;

    /* Public Parameter */
    public float runningSpeed;

    void Awake ()
    {
        /* Initialize Instance (Throw Error If More Than One) */
        if (instance != null)
            Debug.LogError("More than one Player in this scene.");
        else
            instance = this;
    }

    void Start ()
    {
        /* Initialize Components */
        anim = GetComponent<Animator>();   
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        /* Initialize Default Parameters */
        godMode = false;
        walkingSpeed = 3.5f;
        runningSpeed = 8;
        canMove = true;
        isControllable = true;
    }

    void Update ()
    {
        /* If Player is Controllable */
        if (canMove && isControllable)
        {
            /* Get Motion State */
            Motion pendMotion = GetMovement();

            /* Get Any Colliders In Front of Player in New Direction */
            RaycastHit2D hit = Physics2D.Raycast(transform.position, pendMotion.dir, 1, ~LayerMask.GetMask("Default"));

            /* If Movement is Initiated */
            if(pendMotion.speed != 0)
            {
                /* If No Colliders Are In Pending Direction, Move Player */
                if (hit.collider == null)
                    StartCoroutine(Move(pendMotion.dir, pendMotion.speed));

                /* Else (Collider In Front of Pending Direction) */
                else
                {
                    /* If Object is a Mover, Do Event */
                    if (hit.collider.tag == "Mover")
                        hit.collider.gameObject.SendMessage("DoEvent");

                    /* Else, If There is An Obstruction and God Mode is Inactive */
                    else if (!godMode && hit.collider.tag == "Obstacle" || hit.collider.tag == "Interactable")
                    {
                        UpdateAnimation(pendMotion.dir, 3.5f);
                    }
                }
            }

            /* Else (Movement is NOT Initiated) */
            else 
            {
                /* Set Idle Animation */
                UpdateAnimation(pendMotion.dir, 0);

                /* Interact with Object if Check is Performed and is Interactable */
                if(hit.collider != null)
                    if (Input.GetKeyDown(KeyCode.Z) && hit.collider.tag == "Interactable") 
                        hit.collider.gameObject.SendMessage("DoEvent");
                
            }
        }
    }

    private Motion GetMovement ()
    {
        /* Set Default Direction to Current Direction and Speed to To Moving Speed */
        Motion newMotion = new Motion(motion.dir,Input.GetKey(KeyCode.LeftShift) ? 8f : 3.5f);
       
        /* Map Input to New Direction */
        if (Input.GetKey(KeyCode.UpArrow))          newMotion.dir = Vector2.up;
        else if (Input.GetKey(KeyCode.RightArrow))  newMotion.dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))   newMotion.dir = Vector2.down;
        else if (Input.GetKey(KeyCode.LeftArrow))   newMotion.dir = Vector2.left;

        /* Else (No Input is Detected), Set Speed to 0 */
        else newMotion.speed = 0;

        /* Return Motion Instance */
        return newMotion;
    }
}
