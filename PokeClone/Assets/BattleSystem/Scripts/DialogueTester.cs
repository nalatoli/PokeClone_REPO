using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 *  Example Script for Dialogue usage. 
 *  Note some special strings used in the script, for performing special actions:
 *      
 *      <lf>    Go to new line
 *      <br>    Wait for player to continue
 *      <cr>    Wait for player to continue, then clear text
 *      
 *  The format for using special strings is to use them as words, i.e. one space on
 *  each side of the string. For example:
 *      
 *      "Hello <br> <lf> How are you? <cr> Good, I am glad you are doing well!"
 */

public class DialogueTester : MonoBehaviour
{
    /* Private Reference To DialogueManager Script */
    private DialogueManager dialogue;

    void Start()
    {
        /* Assign Reference to Instance of Dialogue Manager (Only One Instance Per Scene) */
        dialogue = DialogueManager.instance;

        /* Begin Coroutine to Begin Dialogue Test Functions */
        StartCoroutine(PerformDialogueTest());
    }

    IEnumerator PerformDialogueTest()
    {
        /* Make Text Box Visible */
        dialogue.SetTextVisibility(true);

        /* Begin By Displaying Single Line at the Start of the Scene Play */
        yield return StartCoroutine(dialogue.PrintText("Hello world and all who inhabit it. "));

        /* Vary Some Parameters of the Text Box */  // ***
        dialogue.printDelay = 0.04f;                // Normal speed of  text
        dialogue.printDelayFast = 1E-4f;            // Speed of text when holding Z
        dialogue.SetColor(Color.magenta);           // Text Color

        /* Using <lf> (Line Feed) to Go to New Line */
        yield return StartCoroutine(dialogue.PrintText("This is a line feed... <lf> "));

        /* Using <br> to Wait for Player Input */
        yield return StartCoroutine(dialogue.PrintText("I'm gonna wait. <br> "));

        /* Regular Text to Test Text Scrolling */
        yield return StartCoroutine(dialogue.PrintText("Scrolling text working perfectly fine? "));

        /* Using <cr> to Clear Text After Waiting For Player Input */
        yield return StartCoroutine(dialogue.PrintText("Alright, i'm going to carriage return right now... <cr> "));

        /* Mark Test Completion */
        yield return StartCoroutine(dialogue.PrintText("Test Complete!"));
    }
}
