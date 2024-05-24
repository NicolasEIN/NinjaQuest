using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private float invincibilityTime = 1.5f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    // Event to notify when health changes
    public event Action<float> OnHealthChanged;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isInvincible)
        {
            currentHealth -= amount;
            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }

            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} morreu.");
        Destroy(gameObject);
    }


}
