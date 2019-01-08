using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SimpleObjectHandler : MonoBehaviour
{
    /* Adjustable Parameters */
    public string text;

    /* Private Parameters */
    private PlayerMovement player;
    private DialogueManager dialouge;

    void Start()
    {
        /* Get Dialogue Box */
        player = PlayerMovement.instance;
        dialouge = DialogueManager.instance;
    }

    void DoEvent ()
    {
        /* Initiate Dialogue */
        StartCoroutine(InitiateDialogue());   
    }

    IEnumerator InitiateDialogue ()
    {
        /* Halt Player Control */
        player.isControllable = false;

        /* Play Sound */
        SoundManager.instance.PlaySound("Blip");

        /* Execute Dialogue */
        dialouge.printDelay = 0.02f;
        dialouge.SetColor(Color.black);
        dialouge.ClearText();
        dialouge.SetTextVisibility(true);
        yield return StartCoroutine(dialouge.PrintText(text));

        /* Wait For User To Continue */
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

        /* Return Control to Player */
        dialouge.SetTextVisibility(false);
        player.isControllable = true;
    }
}
