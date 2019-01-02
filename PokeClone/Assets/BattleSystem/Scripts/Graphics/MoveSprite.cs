using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public GameObject spriteToMove;
    public Vector3 start;
    public Vector3 destination;
    public float speed;

    IEnumerator CR_Move()
    {
        while (spriteToMove.transform.position != destination)
        {
            spriteToMove.transform.position = Vector3.MoveTowards(spriteToMove.transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void Move()
    {
        StartCoroutine("CR_Move");
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteToMove.transform.position = start;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
