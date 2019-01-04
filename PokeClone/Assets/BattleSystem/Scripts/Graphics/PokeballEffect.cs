using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballEffect : MonoBehaviour
{
    public GameObject spriteToScale;
    public GameObject flashMask;
    public ParticleSystem ballOpenEffect;
    public bool emerging;
    public Vector3 minimumScale;
    public Vector3 maximumScale;
    public float scaleSpeed;
    public float flashSpeed;
    public float flashHoldTime;

    private bool scaleUnderway;

    public bool isScaling
    {
        get
        {
            return scaleUnderway;
        }
    }

    private IEnumerator CR_EnterFlash()
    {
        GameObject flash = Instantiate(flashMask);
        flash.GetComponent<SpriteRenderer>().sortingOrder = spriteToScale.GetComponent<SpriteRenderer>().sortingOrder + 1;
        flash.GetComponent<SpriteMask>().sprite = spriteToScale.GetComponent<SpriteRenderer>().sprite;
        flash.transform.position = spriteToScale.transform.position;
        flash.transform.localScale = spriteToScale.transform.localScale;
        flash.transform.parent = spriteToScale.transform;

        Color originalColor = flash.GetComponent<SpriteRenderer>().color;
        Color zeroOpacity = originalColor;
        zeroOpacity.a = 0;

        yield return new WaitForSeconds(flashHoldTime);
        while(flash.GetComponent<SpriteRenderer>().color.a > 0)
        {
            flash.GetComponent<SpriteRenderer>().color = Color.Lerp(flash.GetComponent<SpriteRenderer>().color,
                                                                    zeroOpacity,
                                                                    flashSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(flash);
    }

    private IEnumerator CR_Scale()
    {
        scaleUnderway = true;
        if (emerging)
        {
            spriteToScale.transform.localScale = minimumScale;
            while (spriteToScale.transform.localScale != maximumScale)
            {
                spriteToScale.transform.localScale = Vector3.MoveTowards(spriteToScale.transform.localScale, maximumScale, scaleSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            spriteToScale.transform.localScale = maximumScale;
            while (spriteToScale.transform.localScale != minimumScale)
            {
                spriteToScale.transform.localScale = Vector3.MoveTowards(spriteToScale.transform.localScale, minimumScale, scaleSpeed * Time.deltaTime);
                yield return null;
            }
        }
        scaleUnderway = false;
    }

    public void DisplayPokemonEffect()
    {
        StartCoroutine("CR_Scale");
        StartCoroutine("CR_EnterFlash");
    }
    // Start is called before the first frame update
    void Start()
    {
        scaleUnderway = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
