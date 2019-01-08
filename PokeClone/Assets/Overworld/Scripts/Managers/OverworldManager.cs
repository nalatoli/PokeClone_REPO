using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    /* Public Instance */
    public static OverworldManager instance;

    /* Adjustable Parameter */
    public AreaHandler activeArea;
    public System.DateTime time;

    /* Private Parameters */
    private PlayerMovement player;
    private SoundManager sound;
    private TransitionManager transition;

    void Awake()
    {
        /* Initialize Instance (Throw Error If More Than One) */
        if (instance != null)
            Debug.LogError("More than one Overworld Manager in this scene.");       
        else
            instance = this;

        /* Set Current Time */
        time = System.DateTime.Now;
    }

    void Start()
    {
        /* Initialize Components */
        player = PlayerMovement.instance;
        sound = SoundManager.instance;
        transition = TransitionManager.instance;

        /* Set Current Area as Active and Play Song */
        activeArea.SetState(true);
        StartCoroutine(sound.PlayMusic(activeArea.music));
    }

    public void WarpPlayer(GameObject warpTile)
    {
        /* Warp Player */
        StartCoroutine(StartTransition(warpTile));
    }

    IEnumerator StartTransition (GameObject warpTile)
    {
        /* Screen Fade Out */
        yield return StartCoroutine(transition.PlayTransition(Effect.fadeOut, 2f));

        /* Get Warp Area */
        Transform tmpT = warpTile.transform.parent;
        while (tmpT.tag != "Area") { tmpT = tmpT.parent; }
        AreaHandler warpArea = tmpT.GetComponent<AreaHandler>();

        /* Deactive Old Area */
        activeArea.SetState(false);

        /* Activate New Area */
        warpArea.SetState(true);

        /* Update Active Area */
        activeArea = warpArea;

        /* Play New Song */
        StartCoroutine(sound.PlayMusic(activeArea.music));

        /* If Tile is A Door */
        if (warpTile.tag == "Door")
        {
            /* Transport Player */
            player.transform.position = new Vector2(warpTile.transform.position.x, warpTile.transform.position.y + 0.04f);

            /* Screen Fade In */
            yield return StartCoroutine(transition.PlayTransition(Effect.fadeIn, 2f));

            /* Exit Door */
            yield return StartCoroutine(warpTile.GetComponent<DoorHandler>().ExitDoor());
        }

        /* If Tile is Stairs */
        else if (warpTile.tag == "Stairs")
        {
            /* Transport Player */
            player.transform.position = warpTile.transform.position;

            /* Get DoorHandler Component */
            DoorHandler door = warpTile.GetComponent<DoorHandler>();

            /* Screen Fade In */
            yield return StartCoroutine(transition.PlayTransition(Effect.fadeIn, 2f));

            /* Exit Stairs */
            yield return StartCoroutine(warpTile.GetComponent<StairsHandler>().ExitStairs());
        }

        /* Else (Tile is NOT a Door), Just Trasnport and Screen Fade In */
        else {
            player.transform.position = new Vector2(warpTile.transform.position.x, warpTile.transform.position.y + 0.08f);
            yield return StartCoroutine(transition.PlayTransition(Effect.fadeIn, 2f));
        }

        /* Return Controller To Player */
        player.isControllable = true;
    }
}
