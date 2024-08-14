using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockbackForce = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 knockbackDir = new Vector2(Mathf.Sign(transform.lossyScale.x), 1);
            damageable.Hit(attackDamage, new Vector2(
                knockbackDir.x * knockbackForce.x,
                knockbackDir.y * knockbackForce.y));
        }
    }
}
