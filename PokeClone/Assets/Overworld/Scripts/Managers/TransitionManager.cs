using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* List of Effects */
public enum Effect {
    fadeIn,
    fadeOut,
    openUp
};

public class TransitionManager : MonoBehaviour
{
    /* Public Instance */
    public static TransitionManager instance;

    /* Private Component */
    private Animator anim;

    void Awake ()
    {
        /* Initialize Instance (Throw Error If More Than One) */
        if (instance != null)
            Debug.LogError("More than one Transition Manager in this scene.");
        else
            instance = this;
    }

    void Start()
    {
        /* Get Component */
        anim = GetComponent<Animator>();
    }

    public IEnumerator PlayTransition(Effect effect, float speed)
    {
        /* Update Speed of Transition */
        anim.speed = speed;

        /* Play Transition */
        anim.SetTrigger(effect.ToString());

        /* Wait For Transition To End */
        yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo(0).length / speed);
    }
}
