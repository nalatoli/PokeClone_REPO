using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    /* Adjustable Parameter */
    public GameObject activeArea;

    /* Public Parameters */
    public bool isPlayerControllable;

    /* Private Parameters */
    private PlayerMovement playerScript;

    void Start()
    {
        /* Initialize Parameters */
        isPlayerControllable = true;
        playerScript = FindObjectOfType<PlayerMovement>();
        activeArea.SetActive(true);
    }

    //void Update()
    //{
        
    //}

    public void UpdateActiveArea(GameObject area)
    {
        /* Activate New Area */
        area.SetActive(true);

        /* Deactivate Old Area */
        activeArea.SetActive(false);

        /* Update Area */
        activeArea = area;
    }
}
