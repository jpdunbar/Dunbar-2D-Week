using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigidBody2D;

    public float runSpeed = 600f;
    public SpriteRenderer spriteRenderer;
    public float timer = 0f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        animator.SetBool("isMoving", true);

        timer += Time.deltaTime;

        if (timer < 3f)
        {
            Debug.Log("Here");
            Vector2 direction = new Vector2(runSpeed * Time.deltaTime * -1, 0);
            rigidBody2D.AddForce(direction);
        }
        else
        {
            Debug.Log("There");
            Vector2 direction = new Vector2(runSpeed * Time.deltaTime, 0);
            rigidBody2D.AddForce(direction);
        }

        if (timer > 6f)
        {
            timer = 0f;
        }

        if (rigidBody2D.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
