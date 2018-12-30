using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public GameObject spriteToMove;
    public Transform start;
    public Transform destination;
    public float speed;

    IEnumerator CR_Move()
    {
        if(start != null)
            spriteToMove.transform.position = start.position;

        while (spriteToMove.transform.position != destination.position)
        {
            spriteToMove.transform.position = Vector3.MoveTowards(spriteToMove.transform.position, destination.position, speed * Time.deltaTime);
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
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
