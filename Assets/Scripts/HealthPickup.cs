using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;

    private void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<Damageable>(out var damageable))
        {
            bool isHealled = damageable.Heal(healthRestore);

            if (isHealled)
                Destroy(gameObject);
        }
    }
}
