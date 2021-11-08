using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
    using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody2D;

    public float runSpeed = 5f;
    public float jumpSpeed = 200f;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI intro;
    public TextMeshProUGUI win;
    public Image background;
    private bool menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = true;
        rigidBody2D = GetComponent<Rigidbody2D>();
        //lives.gameObject.SetActive(true);
        //coins.gameObject.SetActive(true);
        intro.gameObject.SetActive(true);
        //win.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int levelMask = LayerMask.GetMask("Level");

            if (Physics2D.BoxCast(transform.position, new Vector2(1, .1f), 0f, Vector2.down, .01f, levelMask))
            {
                Jump();
            }
        }

        if (Input.GetButtonDown("Jump") && menu == true)
        {
            lives.gameObject.SetActive(true);
            coins.gameObject.SetActive(true);
            intro.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
            menu = false;
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(horizontalInput * runSpeed * Time.deltaTime, 0);

        rigidBody2D.AddForce(direction);

        
        if (rigidBody2D.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        if (rigidBody2D.velocity.x > 15)
        {
            rigidBody2D.velocity = new Vector2(10, rigidBody2D.velocity.y);
        }

        if (rigidBody2D.velocity.x < -15)
        {
            rigidBody2D.velocity = new Vector2(-10, rigidBody2D.velocity.y);
        }

        if (Mathf.Abs(horizontalInput) > 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


    }

    void Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HitPlayerLeft"))
        {
            Vector2 moveAway = new Vector2(-800, 0);
            rigidBody2D.AddForce(moveAway);
        }
        if (other.gameObject.CompareTag("HitPlayerRight"))
        {
            Vector2 moveAway = new Vector2(800, 0);
            rigidBody2D.AddForce(moveAway);
        }
        if (other.gameObject.CompareTag("DestroyEnemy"))
        {
            if(other.transform.parent.gameObject != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
        }
    }
}
