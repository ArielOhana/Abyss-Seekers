using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 20f;
    private float jumpingPower = 19f; 
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpingPower);
            animator.SetTrigger("PlayerJump");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 24f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("IsRunning", true);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("PlayerAttack");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("PlayerShield");
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
