/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
    /* Adjustable Parameters */
    public string text;
    public Color textColor;

    /* Private Component */
    private PlayerMovement player;

    void Start ()
    {
        /* Initialize Components */
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        player = PlayerMovement.instance;

        /* Initialize Default Parameter */
        walkingSpeed = 3.5f;
        canMove = true;

        /* Begin Moving Around */
        StartCoroutine(RandomMoveAround());
    }

    void Update ()
    {
        /* Update Sprite Sorting Layer */
        if (transform.position.y < player.transform.position.y)
            renderer.sortingOrder = 1;
        else
            renderer.sortingOrder = -1;
    }

    IEnumerator RandomMoveAround()
    {
        /* Decalre Random Parameters */
        float rand;
        Vector2 randDir;

        while (true)
        {
            /* Wait Between 1 and 10 seconds */
            yield return new WaitForSeconds(Random.Range(1f, 10f));

            /* If NPC Can Move */
            if (canMove)
            {
                /* Get Random Direction */
                rand = Random.value;
                if (rand < 0.25f) randDir = Vector2.left;
                else if (rand < 0.5f) randDir = Vector2.up;
                else if (rand < 0.75f) randDir = Vector2.right;
                else randDir = Vector2.down;

                /* Get Collider in Target Direction */
                RaycastHit2D hit = Physics2D.Raycast(transform.position, randDir, 1, ~LayerMask.GetMask("Default"));

                /* If No Collider, Move NPC */
                if (hit.collider == null)
                    yield return StartCoroutine(Move(randDir, player.walkingSpeed));

                /* Stop Animation */
                UpdateAnimation(randDir, 0); ;

            }
        }
    }

    private void DoEvent()
    {
        /* Face NPC Towards Player and Initiate Dialogue */
        UpdateAnimation(player.motion.dir * -1, 0);
        InitiateDialogue();
    }

    void InitiateDialogue ()
    {
        /* Halt Character Controls */
        player.isControllable = false;
        canMove = false;

        /* Execute Dialogue */
        DialogueManager.instance.printDelay = 0.02f;
        DialogueManager.instance.SetColor(textColor);
        DialogueManager.instance.ClearText();
        DialogueManager.instance.SetTextVisibility(true);
        DialogueManager.instance.PrintText(text);

        /* Wait For Player to End Dialogue */
        StartCoroutine(Wait());
    }

    IEnumerator Wait ()
    {
        /* Wait For Player to End Dialogue */
        yield return new WaitUntil(() => (!DialogueManager.instance.isPrinting && Input.GetKeyDown(KeyCode.Z)));

        /* Return Control to Player */
        DialogueManager.instance.SetTextVisibility(false);
        player.isControllable = true;
        canMove = true;
    }
}
