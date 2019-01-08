/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    /*Adjustable Parameter */
    public GameObject warpTile;
    
    /* Private Components */
    private Animator anim;
    private PlayerMovement player;
    private OverworldManager overworld;
    private new SpriteRenderer renderer;

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

        /* Open Door */
        yield return StartCoroutine(SetOpenState(true));

        /* Move Player Into Door */
        yield return StartCoroutine(player.Move(Vector2.up, player.walkingSpeed));

        /* Stop Player */
        player.UpdateAnimation(Vector2.up, 0f);

        /* Warp Player */
        overworld.WarpPlayer(warpTile);
    }

    public IEnumerator ExitDoor()
    {
        /* Open Door */
        yield return StartCoroutine(SetOpenState(true));

        /* Move Player Away From Door */
        yield return StartCoroutine(player.Move(Vector2.down, player.walkingSpeed));

        /* Stop Player */
        player.UpdateAnimation(Vector2.down, 0);

        /* Close Door */
        yield return StartCoroutine(SetOpenState(false));
    }

    public IEnumerator SetOpenState(bool state)
    {
        /* Play Sound For Opening Door */;
        if(state)
            SoundManager.instance.PlaySound("Door Open");

        /* Animate Door */
        anim.SetBool("isOpen", state);

        /* Wait For Animation To Finish */
        yield return new WaitForSeconds(0.5f);
    }
}
