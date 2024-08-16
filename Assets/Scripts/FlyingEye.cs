using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2;
    public List<Transform> wayPoints;
    private int wayPointIndex;

    Animator animator;
    Rigidbody2D rb;

    public DetectionZone biteZone;
    Damageable damageable;

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget; private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStringHash.hasTarget, value);
        }
    }

    // 不能主动移动,速度为0
    public bool CanMove => animator.GetBool(AnimationStringHash.canMove);
    // 被动无法移动，速度保持不变
    public bool LockVelocity => animator.GetBool(AnimationStringHash.lockVelocity);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive && !LockVelocity)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Flight()
    {
        if (wayPoints.Count == 0)
            return;

        Vector3 targetPosition = wayPoints[wayPointIndex].position;
        Vector3 currentPosition = transform.position;

        SetDirection(targetPosition - currentPosition);

        rb.velocity = Vector2.zero;
        rb.position = Vector3.MoveTowards(currentPosition, targetPosition, flightSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            wayPointIndex++;
            if (wayPointIndex >= wayPoints.Count)
            {
                wayPointIndex = 0;
            }
        }
    }

    void SetDirection(Vector3 dir)
    {
        Vector3 localScale = transform.localScale;
        if (dir.x < 0 && localScale.x > 0
            || dir.x > 0 && localScale.x < 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
