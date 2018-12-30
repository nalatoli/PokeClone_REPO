using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public string message;
    private GameObject line1;
    private GameObject line2;
    private string[] words;
    private Text temp;
    private List<string> splitLines;

    public List<string> GenerateLines(string textString)
    {
        List<string> lines = new List<string>();
        double stringWidth = 0;
        string tempLine = "";
        //words = textString.Split(' ');
        temp.text = textString;
        Debug.Log("temp Text component string: " + temp.text);
        CharacterInfo charInfo = new CharacterInfo();
        Debug.Log(temp.rectTransform.sizeDelta.x);
        Canvas.ForceUpdateCanvases();
        for(int i=0; i<temp.text.Length; i++)
        {
            //Debug.Log("Parse string loop iteration: " + i);
            if(stringWidth < temp.rectTransform.sizeDelta.x)
            {
                temp.font.GetCharacterInfo(temp.text[i], out charInfo, temp.fontSize);
                stringWidth += charInfo.advance * 0.02;
                tempLine += temp.text[i];
                Debug.Log("string width: " + stringWidth);
            }
            else
            {
                lines.Add(tempLine);
                stringWidth = 0;
                tempLine = "";
            }
        }
        return lines;
    }

    public void SetText(string textString)
    {
        Debug.Log("Starting function SetText with string " + textString);
        splitLines = GenerateLines(textString);
    }

    public IEnumerator CR_AdvanceText()
    {
        foreach (string line in splitLines)
        {
            Debug.Log("Current string is: " + line);
            if(line1.GetComponent<Text>().text == "" && line2.GetComponent<Text>().text == "")
            {
                line1.GetComponent<Text>().text = line;
            }
            else if (line1.GetComponent<Text>().text != "" && line2.GetComponent<Text>().text == "")
            {
                line2.GetComponent<Text>().text = line;
            }
            else if (line1.GetComponent<Text>().text != "" && line2.GetComponent<Text>().text != "")
            {
                line1.GetComponent<Text>().text = line2.GetComponent<Text>().text;
                line2.GetComponent<Text>().text = line;
            }
            yield return new WaitForSeconds(3);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        line1 = new GameObject("line1");
        line1.AddComponent<Text>();
        line1.transform.SetParent(transform);
        line1.GetComponent<Text>().font = Font.CreateDynamicFontFromOSFont(Font.GetOSInstalledFontNames()[0], 1);
        line1.GetComponent<Text>().text = "";
        line1.GetComponent<Text>().fontSize = 100;
        line1.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        line1.GetComponent<Text>().rectTransform.pivot = new Vector2(0, 1);
        line1.GetComponent<Text>().rectTransform.localPosition = new Vector3(0, 0, 0);
        line1.GetComponent<Text>().rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        temp = line1.GetComponent<Text>();
        temp.color = Color.clear;
        temp.rectTransform.localPosition = new Vector3(0, -4, 0);

        line2 = new GameObject("line2");
        line2.AddComponent<Text>();
        line2.transform.SetParent(transform);
        line2.GetComponent<Text>().font = Font.CreateDynamicFontFromOSFont(Font.GetOSInstalledFontNames()[0], 1);
        line2.GetComponent<Text>().text = "";
        line2.GetComponent<Text>().fontSize = 100;
        line2.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        line2.GetComponent<Text>().rectTransform.pivot = new Vector2(0, 1);
        line2.GetComponent<Text>().rectTransform.localPosition = new Vector3(0, 0, 0);
        line2.GetComponent<Text>().rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        SetText(message);
        StartCoroutine("CR_AdvanceText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
