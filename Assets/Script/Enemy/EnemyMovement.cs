using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    BoxCollider2D boxCollider2D;

    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb2d.velocity.x)), 1f);
    }
}
