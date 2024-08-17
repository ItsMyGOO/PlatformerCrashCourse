using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 5;
    public float jumpImpulse = 10f;
    Vector2 moveInput;

    public float CurrentMoveSpeed
    {
        get
        {
            if (!CanMove)
                return 0;
            if (!IsMoving)
                return 0;
            if (touchingDirections.IsOnWall)
                return 0;
            if (!touchingDirections.IsGrounded)
                return airWalkSpeed;
            return IsRunning ? runSpeed : walkSpeed;
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStringHash.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStringHash.isRunning, value);
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get => _isFacingRight;
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    // 不能主动移动，速度为0
    public bool CanMove => animator.GetBool(AnimationStringHash.canMove);
    // 被动无法移动，速度保持不变
    public bool LockVelocity => animator.GetBool(AnimationStringHash.lockVelocity);
    public bool IsAlive => animator.GetBool(AnimationStringHash.isAlive);

    public bool AirAttack => animator.GetBool(AnimationStringHash.airAttack);

    Rigidbody2D rb;
    Animator animator;

    TouchingDirections touchingDirections;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        if (CanMove)
            SetFacingDirection(moveInput);
        if (!LockVelocity)
        {
            if (AirAttack)
                rb.velocity = Vector2.zero;
            else
                rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }

        animator.SetFloat(AnimationStringHash.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStringHash.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStringHash.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStringHash.rangedAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}