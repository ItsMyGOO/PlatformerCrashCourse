using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (value <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private bool _isAlive = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStringHash.isAlive, value);
        }
    }

    [SerializeField]
    private bool isInvincible;
    private float timeSinceHit;
    public float invincibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStringHash.hitTrigger);
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.CharacterDamaged?.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (!IsAlive)
            return false;

        if (Health == MaxHealth)
            return false;

        int currentHealth = Health;
        int healthAfterHeal = currentHealth + healthRestore;
        if (healthAfterHeal > MaxHealth)
            healthAfterHeal = MaxHealth;

        healthRestore = healthAfterHeal - currentHealth;

        Health += healthRestore;
        CharacterEvents.CharacterHealed?.Invoke(gameObject, healthRestore);

        return true;
    }
}
