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
    CapsuleCollider2D capCollider2D;

    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float climbSpeed = 5f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log("Move Input: " + moveInput);
    }

    void OnJump(InputValue value)
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        if(value.isPressed && capCollider2D.IsTouchingLayers(groundLayer))
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
        if(capCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
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
}
