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
        InitiateDialogue();   
    }

    void InitiateDialogue()
    {
        /* Halt Player Control */
        player.isControllable = false;

        /* Execute Dialogue */
         dialouge.printDelay = 0.02f;
         dialouge.SetColor(Color.black);
         dialouge.ClearText();
         dialouge.SetTextVisibility(true);
         dialouge.PrintText(text);

        /* Wait For Player to End Dialogue */
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        /* Wait For Player to End Dialogue */
        yield return new WaitUntil(() => (!dialouge.isPrinting && Input.GetKeyDown(KeyCode.Z)));

        /* Return Control to Player */
        dialouge.SetTextVisibility(false);
        player.isControllable = true;
    }

}
