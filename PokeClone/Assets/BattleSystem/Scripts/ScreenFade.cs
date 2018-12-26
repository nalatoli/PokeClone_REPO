using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public Renderer renderer;
    public float fadeInSeconds, fadeOutSeconds;


    IEnumerator CR_Fade()
    {
        renderer.enabled = true;
        Color fadeColor = renderer.material.color;
        for (float f = 0; f <= fadeInSeconds; f += Time.deltaTime)
        {
            fadeColor = renderer.material.color;
            fadeColor.a = f / fadeInSeconds;
            renderer.material.color = fadeColor;
            yield return null;
        }
        for (float f = fadeOutSeconds; f > 0; f -= Time.deltaTime)
        {
            fadeColor = renderer.material.color;
            fadeColor.a = f / fadeOutSeconds;
            renderer.material.color = fadeColor;
            yield return null;
        }
        renderer.enabled = false;
    }

    public void Fade()
    {
        StartCoroutine("CR_Fade");
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
