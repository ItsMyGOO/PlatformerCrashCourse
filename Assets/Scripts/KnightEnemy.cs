using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public float walkSpeed = 3;
    public float walkStopRate = 0.6f;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

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

    public bool CanMove => animator.GetBool(AnimationStringHash.canMove);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        float xVelocity = CanMove ? walkSpeed * walkDirectionVector.x : Mathf.Lerp(rb.velocity.x, 0, walkStopRate);
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
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

    #region ¹¥»÷
    public DetectionZone attackZone;

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget; private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStringHash.hasTarget, value);
        }
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    #endregion
}