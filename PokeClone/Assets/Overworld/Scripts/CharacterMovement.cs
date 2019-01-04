/* Essential Namespaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Motion Handler */
public class Motion
{
    /* Public Members */
    public Vector2 dir;
    public float speed;

    /* Constructor */
    public Motion (Vector2 _dir, float _speed) {
        dir = _dir;
        speed = _speed;
    }

    /* Set Motion */
    public void SetMotion (Vector2 _dir, float _speed) {
        dir = _dir;
        speed = _speed;
    }
}

public class CharacterMovement : MonoBehaviour
{
    /* Direction -> Animation Trigger */
    readonly Dictionary<Vector2, string> dir2trigger = new Dictionary<Vector2, string>() {
        {Vector2.up,    "goUp" },
        {Vector2.right, "goLeft" },
        {Vector2.down,  "goDown" },
        {Vector2.left,  "goLeft" }
    };

    /* Public Parameter */
    public float walkingSpeed;
    public Motion motion = new Motion(Vector2.down, 0);

    /* Shared Components */
    protected Animator anim;
    protected new SpriteRenderer renderer;
    protected new BoxCollider2D collider;

    /* Protected Parameter */
    protected bool canMove;

    public IEnumerator Move (Vector2 dir, float speed)
    {
        /* Increase Box Collider Size */
        collider.size += new Vector2(Mathf.Abs(dir.x) * 0.32f, Mathf.Abs(dir.y) * 0.32f);
        collider.offset += dir * 0.16f;

        /* Update Animation */
        UpdateAnimation(dir, speed);

        /* Translate Player */
        yield return StartCoroutine(Translate(dir));

        /* Decrease Box Collider Size */
        collider.size -= new Vector2(Mathf.Abs(dir.x) * 0.32f, Mathf.Abs(dir.y) * 0.32f);
        collider.offset -= dir * 0.16f;
    }

    public void UpdateAnimation (Vector2 dir, float speed)
    {
        /* If Direction or Speed is Updated */
        if (motion.dir != dir || motion.speed != speed)
        {
            /* Update Motion State */
            motion.dir = dir;
            motion.speed = speed;

            /* Trigger Animation */
            anim.SetFloat("Speed", speed);
            anim.SetTrigger(dir2trigger[dir]);

            /* Flip If Player is Going to the Right */
            if (dir == Vector2.right) renderer.flipX = true;
            else renderer.flipX = false;
        }
    }

    protected IEnumerator Translate (Vector3 dir)
    {
        /* Remove Control of Player */
        canMove = false;

        /* Get Target Position */
        Vector3 targetPos = transform.position + dir;

        /* While The Player is Not At The Target Position */
        while (transform.position.x != targetPos.x || transform.position.y != targetPos.y)
        {
            /* Move Character Towards the Target Position */
            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * motion.speed);
            yield return null;
        }

        /* Return Control To Character */
        canMove = true;
    }
}
