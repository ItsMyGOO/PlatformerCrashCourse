using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public float walkSpeed = 3;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    private WalkbleDirection _walkDirection = WalkbleDirection.Right;
    public WalkbleDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (value != _walkDirection)
            {
                transform.localScale *= new Vector2(-1, 1);

                walkDirectionVector = value switch
                {
                    WalkbleDirection.Right => Vector2.right,
                    WalkbleDirection.Left => Vector2.left,
                    _ => Vector2.zero
                };
            }

            _walkDirection = value;
        }
    }

    private Vector2 walkDirectionVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkbleDirection.Right)
        {
            WalkDirection = WalkbleDirection.Left;
        }
        else if (WalkDirection == WalkbleDirection.Left)
        {
            WalkDirection = WalkbleDirection.Right;
        }
        else
        {
            Debug.LogError("walk direction illegal");
        }
    }

    public enum WalkbleDirection { Right, Left }
}