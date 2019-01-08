using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHandler : MonoBehaviour
{
    /* Adjustable Parameters */
    public Music music;

    public void SetState (bool state)
    {
        /* Set State of Area */
        gameObject.SetActive(state);
    }

}
