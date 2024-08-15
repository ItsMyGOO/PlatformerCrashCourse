using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    Collider2D touchingCollider;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded
    {
        get => _isGrounded;
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStringHash.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get => _isOnWall;
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStringHash.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    public bool IsOnCeiling
    {
        get => _isOnCeiling;
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStringHash.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 wallCheckDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCollider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}