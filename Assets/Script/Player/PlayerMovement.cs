using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    SpriteRenderer spriteRenderer;

    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float gravityAtStart = 1f;
    [SerializeField] Color32 deadColor = new Color32(255, 255,255, 255);


    bool isAlive = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>(); 
        myFeetCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        //Debug.Log("Move Input: " + moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        if(value.isPressed && myFeetCollider.IsTouchingLayers(groundLayer))
        {
            rb2d.velocity += new Vector2(0f, jumpStrength);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void ClimbLadder()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.gravityScale = 0f;

            animator.SetBool("isClimbing", Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon);
        }
        else
        {
            rb2d.gravityScale = gravityAtStart;
            animator.SetBool("isClimbing", false);
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        }
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            spriteRenderer.color = deadColor;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpStrength);
            isAlive = false;
            animator.SetTrigger("Dying");
        }
    }
}
