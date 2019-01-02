using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    /* Public Instance */
    public static DialogueManager instance;

    /* Game Objects */
    public GameObject textContainer;
    public GameObject scrollerContainer;
    public TextMeshProUGUI textBox;

    /* Adjustable Parameters */
    public float printDelay;
    public float printDelayFast;

    /* Public Parameter */
    public bool isPrinting;

    /* Private Parameters */
    private ScrollRect scroller;
    private uint lines;
    private bool isZpressed;

    void Awake ()
    {
        /* Initialize Instance (Throw Error If More Than One) */
        if (instance != null)
            Debug.LogError("More than one Dialogue Manager in this scene.");
        else
            instance = this;
    }

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
        isZpressed = false;
    }

    public void SetTextVisibility(bool state)
    {
        /* Set State of Dialogue Box */
        textContainer.SetActive(state);
    }

    public void ClearText()
    {
        /* Reset Text */
        textBox.text = "";
        scroller.verticalNormalizedPosition = 1;
        lines = 1;
    }

    public void SetColor(Color color)
    {
        /* Set Color of Text */
        textBox.color = color;
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

        /* Get Each Word in Dialogue */
        foreach (string word in text.Split(' '))
        {
            /* If String is a Carriage Return */
            if (word == "<cr>")
            {
                /* Wait For User to Continue */
                yield return StartCoroutine(MoveArrow());

                /* Reset Text Box */
                ClearText();
            }

            /* If String is a Break */
            else if (word == "<br>")
            {
                /* Wait For User to Continue */
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            }

            /* If String is a Line Feed */
            else if (word == "<lf>")
            {
                /* If More than 2 Lines */
                if (++lines > 2)
                {
                    /* Wait for User Then Scroll */
                    yield return StartCoroutine(MoveArrow());
                    yield return StartCoroutine(Scroll());
                }

                /* Add New Line */
                textBox.text += '\n';
            }

            /* Else (Word is NOT Special) */
            else
            {
                /* Print Invisible Word and Force Onto Text Box */
                string wordBuff = "<color=#00000000>" + word + " </color>";
                textBox.text += wordBuff;
                Canvas.ForceUpdateCanvases();

                /* Remove Invisible Word from Buffer and Clear Word Buffer */
                textBox.text = textBox.text.Substring(0, textBox.text.Length - wordBuff.Length);
                wordBuff = "";

                /* Check If New Line Has Been Created */
                if (lines != textBox.textInfo.lineCount)
                {
                    /* Add NewLine to Word Buffer */
                    wordBuff += '\n';

                    /* If More Than 2 Lines Are Detected */
                    if (++lines > 2)
                    {
                        /* Wait for User Then Scroll */
                        yield return StartCoroutine(MoveArrow());
                        yield return StartCoroutine(Scroll());
                    }
                }

                /* Add Word and Space to Word Buffer */
                wordBuff += word + ' ';

                /* For Each Character in Word Buffer */
                foreach (char c in wordBuff)
                {
                    /* Print Character */
                    textBox.text += c;

                    /* Wait (Faster Wait if 'Z' is Held) */
                    yield return new WaitForSeconds(Input.GetKey(KeyCode.Z) ? printDelayFast : printDelay);
                }
            }
        }

        /* Indicate That Printing is Over */
        isPrinting = false;
    }

    IEnumerator Scroll()
    {
        /* While Scroll Bar Has NOT Reached New Line */
        float targetScrollPos = scroller.verticalNormalizedPosition - 0.13f;
        while (scroller.verticalNormalizedPosition >= targetScrollPos)
        {
            /* Scroll Text */
            scroller.verticalNormalizedPosition -= 0.01f;
            yield return new WaitForSeconds(1E-2f);
        }
    }

    IEnumerator MoveArrow ()
    {
        /* Print Arrow and Initialize Offset */
        textBox.text += "  " + "<sprite=\"arrow\" index=2>";
        isZpressed = false;
        float off = 2;
        bool isDown = true;
        
        while (!isZpressed)
        {
            /* Wait */
            yield return StartCoroutine(Wait(0.2f));

            /* Update Offset */
            off += isDown ? -1 : 1;
            if (off == 0) isDown = false;
            else if (off == 2) isDown = true;

            /* Update Arrow */
            textBox.text = textBox.text.Substring(0, textBox.text.Length - "<sprite=\"arrow\" index=2>".Length);
            textBox.text += "<sprite=\"arrow\" index=" + off.ToString() + ">";
        }

        /* Remove Arrow and Clear Z-Pressed Flag */
        textBox.text = textBox.text.Substring(0, textBox.text.Length - "<sprite=\"arrow\" index=2>".Length);
        isZpressed = false;
    }

    IEnumerator Wait(float timeOut)
    {
        /* Wait For Key or TimeOut */
        while (!Input.GetKey(KeyCode.Z)) {
            yield return null;
            timeOut -= Time.deltaTime;
            if (timeOut <= 0f) break;
        }

        /* Assign Z-Press Buffer */
        isZpressed = timeOut <= 0 ? false : true;
    }
}

