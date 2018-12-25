/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***************************************************************************************** 
 * The 'StateController' class controls the state of the player. The state constitutes
 * the speed and animations of the player. The speed is adjustable publically.
 * The speed and animations will NOT apply to the player until the player is MOVABLE.
 *  
 ****************************************************************************************/
public class StateController : MonoBehaviour
{
    /* Enumeration of States */
    public enum State {
        idle,
        walking,
        running,
        biking,
        surfing
    };

    /* Direction -> Animation Trigger */
    Dictionary<Vector3, string> dir2trigger = new Dictionary<Vector3, string>() {
        {Vector3.up,    "goUp" },
        {Vector3.right, "goRight" },
        {Vector3.down,  "goDown" },
        {Vector3.left,  "goLeft" }
    };

    /* State -> (Animation) Speed */
    Dictionary<State, float> state2speed = new Dictionary<State, float>() {
        {State.idle,    0 },
        {State.walking, 3 },
        {State.running, 6 },
        {State.biking,  8 },  
        {State.surfing, 8 }
    };

    /* Private Parameters */
    private State state;
    private Animator anim;
    private PlayerMovement player;


    void Start()
    {
        /* Initialize Parameters */
        state = State.idle;
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        /* Get State of Player */
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
            state = Input.GetKey(KeyCode.LeftShift) ? State.running : State.walking;

        else
            state = State.idle;


        /* If Player is Movable */
        if(player.isMovable)
        {
            /* Update Animation and Speed of Player */
            anim.SetFloat("Speed", state2speed[state]);
            anim.SetTrigger(dir2trigger[player.dir]);
            player.speed = state2speed[state];
        }
    }
}
