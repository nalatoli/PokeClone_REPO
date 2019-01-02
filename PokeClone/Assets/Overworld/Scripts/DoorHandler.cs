/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************** 
 * The 'DoorHandler' class controls the warp of the player. First, the door animates, and
 * forcibly moves the player towards the door. Then, the player will warp to a new 
 * area depending on the adjustable parameter 'targetLocation'. 
 *  
 ****************************************************************************************/
public class DoorHandler : MonoBehaviour
{
    /*Adjustable Parameter */
    public GameObject warpTile;
    
    /* Private Components */
    private Animator anim;
    private PlayerMovement player;
    private OverworldManager overworld;

    /* Private Parameters */
    private Vector2 warpPosition;
    private GameObject warpArea;

    void Start ()
    {
        /* Initialize Components */
        anim = GetComponent<Animator>();
        player = PlayerMovement.instance;
        overworld = OverworldManager.instance;
    }

    void DoEvent()
    {
        /* Forcibly Move Player to Door Entrace */
        StartCoroutine(EnterDoor());
    }

    IEnumerator EnterDoor ()
    {
        /* Halt Control Over the Player */
        player.isControllable = false;

        /* Face Player Towards Door */
        player.UpdateAnimation(Vector2.up, 0f);

        /* Start Door Open Animation */
        anim.SetBool("isOpen", true);

        /* Wait A Little For Door To Open */
        yield return new WaitForSeconds(0.5f);

        /* Move Player Into Door */
        yield return StartCoroutine(player.Move(Vector2.up, player.walkingSpeed));

        /* Stop Player */
        player.UpdateAnimation(Vector2.up, 0f);

        /* Warp Player */
        overworld.WarpPlayer(warpTile);
    }
}
