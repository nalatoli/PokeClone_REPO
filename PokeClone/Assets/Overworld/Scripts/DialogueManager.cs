using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    /* Game Objects */
    public GameObject textContainer;
    public GameObject scrollerContainer;
    public Text textBox;

    /* Adjustable Parameters */
    public float printDelay;
    public float printDelayFast;

    /* Public Parameter */
    public bool isPrinting;

    /* Private Parameters */
    private ScrollRect scroller;
    private uint lines;

    void Start()
    {
        /* Get Component */
        scroller = scrollerContainer.GetComponent<ScrollRect>();

        /* Reset Dialogue Box */
        isPrinting = false;
        SetTextVisibility(false);
        ClearText();

        /* Set Default Parameters */
        printDelay = 0.02f;
        printDelayFast = 1E-5f;
        
    }


    public void SetTextVisibility(bool state)
    {
        /* Set State of Dialogue Box */
        textContainer.SetActive(state);
    }

    public void ClearText()
    {
        /* Reset Text */
        textBox.text = null;
        scroller.verticalNormalizedPosition = 1;
        lines = 1;
    }

    public void SetColor(byte r, byte g, byte b)
    {
        /* Set Color of Text */
        textBox.color = new Color((r <= 255) ? r : 255 / 255f, (g <= 255) ? g : 255 / 255f, (b <= 255) ? b : 255 / 255f);
    }

    public void PrintText(string text)
    {
        /* Start Displaying the Text */
        StartCoroutine(DisplayText(text));
    }

    IEnumerator DisplayText(string text)
    {
        /* Initialize Parameters */
        isPrinting = true;
        string tmpStr;

        /* Get Each Word in Dialogue */
        foreach (string word in text.Split(' '))
        {
            /* Initialize New Line Marker to False */
            bool isNewLine = false;

            /* If String is a Carriage Return */
            if (word == "<cr>")
            {
                 /* Wait For User to Continue */
                // make little arrow appear
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

                /* Reset Line Counter/Scroll/Text */
                lines = 1;
                scroller.verticalNormalizedPosition = 1;
                textBox.text = null;
            }

            /* If String is a Break */
            else if (word == "<br>")
            {
                /* Wait For User to Continue */
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            }

            /* If String is a Break */
            else if (word == "<lf>")
            {
                /* Increment Line Count and Mark New Line */
                lines++;
                isNewLine = true;
                textBox.text += '\n';
            }

            /* Else (Word is NOT Special) */
            else
            {
                /* Display Invisible Word and Force Canvas Update */
                tmpStr = "<color=#00000000>" + word + " </color>";
                textBox.text += tmpStr;
                Canvas.ForceUpdateCanvases();

                /* Check If New Line Has Been Created */
                if(lines != textBox.cachedTextGenerator.lineCount)
                {
                    /* Mark That New Line Has Occured */
                    isNewLine = true;

                    /* If More Than 2 Lines Are Detected */
                    if (++lines > 2)
                    {
                        /* Wait For User to Continue */
                        // make little arrow appear
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

                        /* While Scroll Bar Has NOT Reached New Line */
                        float targetScrollPos = scroller.verticalNormalizedPosition - 0.13f;
                        while (scroller.verticalNormalizedPosition >= targetScrollPos)
                        {
                            /* Scroll Text */
                            scroller.verticalNormalizedPosition -= 0.01f;
                            yield return new WaitForSeconds(1E-2f);
                        }
                    }
                }

                /* Remove Invisible Word */
                textBox.text = textBox.text.Substring(0, textBox.text.Length - tmpStr.Length);

                /* Modify Word */
                if (isNewLine) tmpStr = '\n' + word + ' ';
                else           tmpStr = word + ' ';

                /* Print Word */
                foreach (char c in tmpStr) {
                    textBox.text += c;
                    yield return new WaitForSeconds(Input.GetKey(KeyCode.Z) ? printDelayFast : printDelay);
                }
            }
        }

        /* Indicate That Printing is Over */
        isPrinting = false;
    }
}
