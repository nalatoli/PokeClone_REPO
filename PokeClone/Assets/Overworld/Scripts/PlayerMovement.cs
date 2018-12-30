/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************** 
 * The 'PlayerMovement' class controls the movement of the player. The player can walk,
 * run, bike, and surf in the four cardinal directions (North, South, East, and West). 
 * The direction of motion is determined by the user key input at the start of every
 * overworld frame. The type of motion is determined by the state of the player and 
 * the key input at the given overworld frame.
 *  
 ****************************************************************************************/
public class PlayerMovement : MonoBehaviour
{
    /* Direction -> Animation Trigger */
    Dictionary<Vector2, string> dir2trigger = new Dictionary<Vector2, string>() {
        {Vector2.up,    "goUp" },
        {Vector2.right, "goRight" },
        {Vector2.down,  "goDown" },
        {Vector2.left,  "goLeft" }
    };

    /* Motion Handler */
    public class Motion {
        public Vector2 dir;
        public float speed;
    }

    /* Public Parameter */
    public float walkingSpeed;
    public float runningSpeed;

    /* Private Parameters */
    private Animator anim;
    private OverworldManager overworld;
    private Motion current = new Motion();
    private Motion previous = new Motion();
    private Vector2 targetPos;

    void Start ()
    {
        /* Initialize Parameters */
        walkingSpeed = 3;
        runningSpeed = 6;
        anim = GetComponent<Animator>();
        overworld = FindObjectOfType<OverworldManager>();
        current.dir = Vector2.down; current.speed = 0;
        previous.dir = current.dir; previous.speed = current.speed;
        targetPos = transform.position;
    }

    void Update ()
    {
        /* If Player is Controllable */
        if(overworld.isPlayerControllable)
        {
            /* Declare Parameters */
            RaycastHit2D hit;
            bool isMovementInitiated = true;

            /* Get Direction of Player */
            if      (Input.GetKey(KeyCode.UpArrow))     { current.dir = Vector2.up;}
            else if (Input.GetKey(KeyCode.RightArrow))  { current.dir = Vector2.right; }
            else if (Input.GetKey(KeyCode.DownArrow))   { current.dir = Vector2.down; }
            else if (Input.GetKey(KeyCode.LeftArrow))   { current.dir = Vector2.left; }
            else                                        { current.speed = 0; isMovementInitiated = false; }

            /* Get Any Colliders In Front of Player */
            hit = Physics2D.Raycast(transform.position, current.dir, 1, ~LayerMask.GetMask("Default"));

            /* If Movement Key Is Pressed */
            if (isMovementInitiated)
            {
                /* Update Speed */
                current.speed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed;

                /* If There is A Collider In Front of Player */
                if (hit.collider != null)
                {
                    /* If Collider is Mover */
                    if (hit.collider.tag == "Mover")
                    {
                        /* Perform Mover Event */
                        hit.collider.gameObject.SendMessage("doEvent", gameObject);
                        current.speed = 0;
                    }

                    /* Else, Update Target Position if No Obstruction */
                    else if (hit.collider.tag != "Obstacle" && hit.collider.tag != "Interactable")
                        StartCoroutine(MovePlayer());
                }

                /* Else (No Collider In Front of Player), Update Target Position */
                else { StartCoroutine(MovePlayer()); }
 
            }

            /* Else, If Check is Executed and Collider is In Front of Player */
            else if(Input.GetKeyDown(KeyCode.Z) && hit.collider != null)
            {
                /* If Collider is Interactable */
                if(hit.collider.tag == "Interactable")
                {
                    /* Execute Event Assigned to Interactable */
                    hit.collider.gameObject.SendMessage("doEvent");
                }
            }

            /* If Direction or Speed Has Changed */
            if(current.dir != previous.dir || current.speed != previous.speed)
            {
                /* Update Parameters and Animation */
                previous.dir = current.dir;
                previous.speed = current.speed;
                anim.SetFloat("Speed", current.speed);
                anim.SetTrigger(dir2trigger[current.dir]);
            }
        }      
    }

    IEnumerator MovePlayer ()
    {
        /* Update Target Position and Remove Control Of Player */
        targetPos += current.dir;
        overworld.isPlayerControllable = false;

        /* While The Player is Not At The Target Position */
        while (transform.position.x != targetPos.x || transform.position.y != targetPos.y)
        {
            /* Move Player Towards the Target Position */
            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * current.speed);
            yield return null;
        }

        /* Return Control To Player */
        overworld.isPlayerControllable = true;
    }
}
