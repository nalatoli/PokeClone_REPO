using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    new public Renderer renderer;
    public float fadeInSeconds, fadeOutSeconds;

    private Color fadeColor;
    //need this so the image used for the screen fade doesn't exceed its
    //original transparency when the fully faded in
    private float originalAlpha;
    private bool fadeUnderway = false;
    public bool isFading
    {
        get
        {
            return fadeUnderway;
        }
    }

    IEnumerator CR_Fade()
    {
        fadeUnderway = true;
        renderer.enabled = true;

        //increment the alpha value of the screen fade image to the original level over fadeInSeconds seconds
        for (float f = 0; f <= fadeInSeconds; f += Time.deltaTime)
        {
            fadeColor = renderer.material.color;
            fadeColor.a = f / fadeInSeconds * originalAlpha;
            renderer.material.color = fadeColor;
            yield return null;
        }
        //decrement the alpha value of the screen fade image to the zero over fadeOutSeconds seconds
        for (float f = fadeOutSeconds; f > 0; f -= Time.deltaTime)
        {
            fadeColor = renderer.material.color;
            fadeColor.a = f / fadeOutSeconds * originalAlpha;
            renderer.material.color = fadeColor;
            yield return null;
        }
        fadeUnderway = false;
        //the renderer is disabled because the image still shows through a bit at min opacity
        renderer.enabled = false;
    }

    public void Fade()
    {
        if(!fadeUnderway)
            StartCoroutine("CR_Fade");
    }


    // Start is called before the first frame update
    void Start()
    {
        fadeColor = renderer.material.color;
        originalAlpha = renderer.material.color.a;
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
