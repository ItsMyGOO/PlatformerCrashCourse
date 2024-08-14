using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockbackForce = Vector2.zero;

    public bool selfDestroy = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damageable>(out var damageable))
        {
            Vector2 knockbackDir = new(Mathf.Sign(transform.lossyScale.x), 1);
            bool hit = damageable.Hit(attackDamage, new Vector2(
                   knockbackDir.x * knockbackForce.x,
                   knockbackDir.y * knockbackForce.y));

            if (hit && selfDestroy)
                Destroy(gameObject);
        }
    }
}
