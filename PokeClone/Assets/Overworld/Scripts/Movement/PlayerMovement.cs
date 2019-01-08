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

    /* Private Components */
    private new SoundManager audio;

    /* Private Parameters */
    private bool bumpOnCooldown;

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
        audio = SoundManager.instance;

        /* Initialize Default Parameters */
        godMode = false;
        walkingSpeed = 3.5f;
        runningSpeed = 8;
        canMove = true;
        isControllable = true;
        bumpOnCooldown = false;
    }

    void Update ()
    {
        /* If Player is Controllable */
        if (canMove && isControllable)
        {
            /* Get Motion State */
            Motion pendMotion = GetMovement();

            /* Get Any Colliders In Front of Player in New Direction */
            RaycastHit2D hit = Physics2D.Raycast(transform.position, pendMotion.dir, 0.16f, ~LayerMask.GetMask("Default"));

            /* If Movement is Initiated */
            if (pendMotion.speed != 0)
            {
                /* If No Colliders Are In Pending Direction, Move Player */
                if (hit.collider == null)
                    StartCoroutine(Move(pendMotion.dir, pendMotion.speed));
                
                /* Else (Collider In Front of Pending Direction) */
                else
                {
                    /* Get Layer */
                    string layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);

                    /* If Object is a Mover, Do Event */
                    if (layerName == "Mover") {
                        UpdateAnimation(pendMotion.dir, 0);
                        hit.collider.gameObject.SendMessage("DoEvent");
                    }

                    /* Else, If There is An Obstruction/Character and God Mode is Inactive */
                    else if (!godMode && layerName == "Obstacle" || layerName == "Obstacle" || layerName == "Interactable")
                    {
                        /* Stop Player and Do Bumping */
                        UpdateAnimation(pendMotion.dir, 3.5f);
                        if (!bumpOnCooldown)
                            StartCoroutine(PlayBumpSound());
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
                    if (Input.GetKeyDown(KeyCode.Z) && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Interactable") 
                        hit.collider.gameObject.SendMessage("DoEvent");
                
            }
        }
    }

    private IEnumerator PlayBumpSound ()
    {
        /* Mark Bump On Cooldown */
        bumpOnCooldown = true;
        
        /* Play Bump Sound */
        audio.PlaySound("Bump");

        /* Wait */
        yield return new WaitForSeconds(1f / walkingSpeed);

        /* Mark Bump Ready */
        bumpOnCooldown = false;      
    }

    private Motion GetMovement ()
    {
        /* Set Default Direction to Current Direction and Speed to To Moving Speed */
        Motion newMotion = new Motion(motion.dir,Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed);
       
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
