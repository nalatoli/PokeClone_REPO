using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsHandler : MonoBehaviour
{
    public enum Direction { up = 1, down = -1 }

    /*Adjustable Parameter */
    public GameObject otherStairs;
    public Direction direction;

    /* Private Components */
    private OverworldManager overworld;
    private PlayerMovement player;

    void Start ()
    {
        /* Initialize Components */
        overworld = OverworldManager.instance;
        player = PlayerMovement.instance;
    }

    void DoEvent ()
    {
        /* Forcibly Move Player to Stairs */
        StartCoroutine(EnterStairs());
    }

    public IEnumerator EnterStairs ()
    {
        /* Halt Control Over the Player */
        player.isControllable = false;

        /* Walk 3 steps */
        for (int i = 0; i < 3; i++)
        {           
            yield return StartCoroutine(player.Move(player.motion.dir, 2f, 0.393f));
            player.transform.position += Vector3.up * 0.196f * (float)direction;
        }

        /* Play Sound */
        SoundManager.instance.PlaySound("Exit Area");

        /* Warp Player */
        overworld.WarpPlayer(otherStairs);
    }

    public IEnumerator ExitStairs ()
    {
        /* Walk 3 steps */
        for (int i = 0; i < 3; i++)
        {
            player.transform.position += Vector3.down * 0.196f * (float)direction;
            yield return StartCoroutine(player.Move(player.motion.dir, 2f, 0.393f));           
        }
    }
}
