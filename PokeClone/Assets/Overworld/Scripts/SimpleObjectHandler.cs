using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/***************************************************************************************** 
 * The 'SimpleObjectHandler' class controls the text displayed when checking a simple
 * object.
 *  
 ****************************************************************************************/
public class SimpleObjectHandler : MonoBehaviour
{
    /* Adjustable Parameters */
    public string text;

    /* Private Parameters */
    private OverworldManager overworld;
    private DialogueManager dialogue;

    void Start()
    {
        /* Get Dialogue Box */
        overworld = FindObjectOfType<OverworldManager>();
        dialogue = FindObjectOfType<DialogueManager>();
    }

    void doEvent ()
    {
        /* Halt Player Control */
        overworld.isPlayerControllable = false;

        /* Execute Dialogue */
        dialogue.printDelay = 0.02f;
        dialogue.SetColor(0, 0, 0);
        dialogue.ClearText();
        dialogue.SetTextVisibility(true);
        dialogue.PrintText(text);

        /* Wait For Player to End Dialogue */
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        /* Wait For Player to End Dialogue */
        yield return new WaitUntil(() => (!dialogue.isPrinting && Input.GetKeyDown(KeyCode.Z)));

        /* Return Control to Player */
        dialogue.SetTextVisibility(false);
        overworld.isPlayerControllable = true;
    }

}
