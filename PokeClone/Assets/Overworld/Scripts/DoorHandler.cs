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
    public Vector3 warpLocation;
    
    /* Private Parameters */
    private Animator anim;
    private PlayerMovement playerScript;
    private Animator playerAnim;
    private OverworldManager overworld;

    void Start()
    {
        /* Initialize Parameters */
        anim = GetComponent<Animator>();
        overworld = FindObjectOfType<OverworldManager>();
    }

    void doEvent(GameObject player)
    {
        /* Get Access To Player Movement/Animatior */
        playerScript = player.GetComponent<PlayerMovement>();
        playerAnim = player.GetComponent<Animator>();

        /* Forcibly Move Player to Door Entrace */
        StartCoroutine(MovePlayer(player));
    }

    IEnumerator MovePlayer (GameObject player)
    {
        /* Halt Control Over the Player */
        overworld.isPlayerControllable = false;

        /* Start Door Open Animation */
        anim.SetBool("isOpen", true);

        /* Wait A Little For Door To Open */
        yield return new WaitForSeconds(0.5f);

        /* Animate Player */
        playerAnim.SetFloat("Speed", playerScript.walkingSpeed);
        playerAnim.SetTrigger("goUp");

        /* Move Player Toward Door */
        while (player.transform.position != transform.position)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, Time.deltaTime * playerScript.walkingSpeed);
            yield return null;
        }

        /* Stop Player Animation */
        playerAnim.SetFloat("Speed", 0);
        playerAnim.SetTrigger("goUp");

        /* Warp Player */
        player.transform.position = warpLocation;

        /* Return Control To Player */
        overworld.isPlayerControllable = true;

    }
}
