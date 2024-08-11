using System.Collections;
using System.Collections.Generic;
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
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool isHealled = damageable.Heal(healthRestore);

            if (isHealled)
                Destroy(gameObject);
        }
    }
}
