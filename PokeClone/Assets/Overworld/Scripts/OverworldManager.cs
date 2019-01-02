using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    /* Public Instance */
    public static OverworldManager instance;

    /* Adjustable Parameter */
    public GameObject activeArea;

    /* Private Parameters */
    private PlayerMovement player;
    private SoundManager sound;


    void Awake()
    {
        /* Initialize Instance (Throw Error If More Than One) */
        if (instance != null)
            Debug.LogError("More than one Overworld Manager in this scene.");       
        else
            instance = this;
    }

    void Start()
    {
        /* Initialize Components */
        player = PlayerMovement.instance;
        sound = SoundManager.instance;

        /* Set Current Area as Active */
        activeArea.SetActive(true);
    }

    public void WarpPlayer (GameObject warpTile)
    {
        /* Start Transitioning Player */
        StartCoroutine(StartTransition(warpTile));
    }

    IEnumerator StartTransition(GameObject warpTile)
    {
        /* Halt Control Over the Player */
        player.isControllable = false;

        /* Get Warp Area */
        Transform tmpT = warpTile.transform.parent;
        while (tmpT.tag != "Area") { tmpT = tmpT.parent; }
        GameObject warpArea = tmpT.gameObject;

        /* Screen Fade Out */
        // ...
        yield return new WaitForSeconds(0.5f);

        /* Activate New Area */
        warpArea.SetActive(true);

        /* Transport Player */
        player.transform.position = warpTile.transform.position;

        /* Deactive Old Area */
        activeArea.SetActive(false);

        /* Update Active Area */
        activeArea = warpArea;

        /* Screen Fade In */
        // ...
        yield return new WaitForSeconds(0.5f);

        /* Return Control To Player */
        player.isControllable = true;
    }
}
