using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public float walkSpeed = 3;
    public float walkStopRate = 0.6f;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    private WalkbleDirection _walkDirection;
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
    private Vector2 walkDirectionVector = Vector2.right;

    // 不能主动移动,速度为0
    public bool CanMove => animator.GetBool(AnimationStringHash.canMove);
    // 被动无法移动，速度保持不变
    public bool LockVelocity => animator.GetBool(AnimationStringHash.lockVelocity);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!LockVelocity)
        {
            if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
                FlipDirection();

            //float xVelocity = CanMove ? walkSpeed * walkDirectionVector.x : Mathf.Lerp(rb.velocity.x, 0, walkStopRate);
            float xVelocity = CanMove ? walkSpeed * walkDirectionVector.x : 0;
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        }
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

    #region 攻击
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

    public float AttackCooldown
    {
        get => animator.GetFloat(AnimationStringHash.attackCooldown);
        set => animator.SetFloat(AnimationStringHash.attackCooldown, value);
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    #endregion
}