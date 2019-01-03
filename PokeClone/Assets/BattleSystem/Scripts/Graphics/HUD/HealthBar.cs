using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer healthBarRenderer;

    private Vector3 initialPosition;
    private float initialScale;
    private float fullLength;
    // Start is called before the first frame update

    public void SetFullness(float amount)
    {
        //Amount should be normalized from 0 to 1
        if (amount < 0)
            amount = 0;
        else if (amount > 1)
            amount = 1;

        if (amount > 0.5f)
            healthBarRenderer.color = Color.green;
        else if (amount <= 0.5f && amount > 0.25f)
            healthBarRenderer.color = Color.yellow;
        else
            healthBarRenderer.color = Color.red;

        //calculate new center of health bar
        //this is needed because the sprite is scaled from its center, which causes the bar to
        //appear to not deplete to the lefthand side
        Vector3 newLocalPosition = initialPosition;
        newLocalPosition.x -= ((fullLength / 2.0f) * (1 - amount));
        healthBarRenderer.transform.localPosition = newLocalPosition;

        //only the x-axis scale factor changes
        healthBarRenderer.transform.localScale = new Vector3(initialScale * amount,
                                                     healthBarRenderer.transform.localScale.y,
                                                     healthBarRenderer.transform.localScale.z);


    }

    void Start()
    {
        //The scale factor on the x axis of the sprite used by the health bar at full health
        initialScale = healthBarRenderer.transform.localScale.x;

        //The absolute width of the health bar at full health
        fullLength = healthBarRenderer.bounds.size.x;
        //The width after being scaled relative to a parent
        if(healthBarRenderer.transform.parent != null)
            fullLength /= healthBarRenderer.transform.parent.localScale.x;
 

        //The center position of the health bar relative to parent at full health
        initialPosition = healthBarRenderer.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
