using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdater : MonoBehaviour
{
    /* Private Components */
    private new SpriteRenderer renderer;
    private PlayerMovement player;

    void Start()
    {
        /* Get Components */
        renderer = GetComponent<SpriteRenderer>();
        player = PlayerMovement.instance;

        /* Forcible Update Sorting Layer If Neccessary */
        renderer.sortingLayerID = player.gameObject.GetComponent<Renderer>().sortingLayerID;
    }

    void Update()
    {
        /* Update Sprite Sorting Layer */
        renderer.sortingOrder = (transform.position.y < player.transform.position.y) ? 1 : -1;
    }
}
