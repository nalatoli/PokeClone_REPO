using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBuildingHandler : MonoBehaviour
{
    /*Adjustable Parameter */
    public GameObject warpTile;
    private PlayerMovement player;

    /* Private Components */
    private OverworldManager overworld;

    void Start ()
    {
        /* Initialize Components */
        overworld = OverworldManager.instance;
        player = PlayerMovement.instance;
    }

    void DoEvent ()
    {
        /* Halt Control Over the Player */
        player.isControllable = false;

        /* Play Sound */
        SoundManager.instance.PlaySound("Exit Area");

        /* Warp Player */
        overworld.WarpPlayer(warpTile);
    }
}
