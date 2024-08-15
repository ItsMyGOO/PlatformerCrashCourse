using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new(3, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }
}
