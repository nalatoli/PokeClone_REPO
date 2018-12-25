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
    /* Adjustable Parameters */
    public float speed;
    public bool isControllable;
    
    /* Public Parameters */
    public Vector3 dir;
    public bool isMovable;

    /* Private Parameter */
    private Vector3 targetPos;

    void Start ()
    {
        /* Initialize Parameters */
        dir = Vector3.down;
        targetPos = transform.position;
    }

    void Update ()
    {
        /* Determine if Player is Movable */
        isMovable = (transform.position == targetPos) && isControllable;
        // ...
      
        /* If Player is Controllable */
        if(isMovable)
        {
            /* Get Direction of Player */
            if      (Input.GetKey(KeyCode.W)) { dir = Vector3.up;       targetPos += dir; }
            else if (Input.GetKey(KeyCode.D)) { dir = Vector3.right;    targetPos += dir; }
            else if (Input.GetKey(KeyCode.S)) { dir = Vector3.down;     targetPos += dir; }
            else if (Input.GetKey(KeyCode.A)) { dir = Vector3.left;     targetPos += dir; }
        }

        /* Move Player */
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
