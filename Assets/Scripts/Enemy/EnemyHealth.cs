using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private float invincibilityTime = 1.5f; // Tempo de invencibilidade após ser atingido
    private bool isInvincible = false; // Flag para controlar o estado de invencibilidade
    private float invincibilityTimer = 0f; // Contador de tempo de invencibilidade

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false; // Desativa o estado de invencibilidade quando o tempo acabar
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isInvincible)
        {
            Debug.Log($"{gameObject.name} Vida atual:  {currentHealth}");
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Die();
            }

            // Ativa o estado de invencibilidade
            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    private void Die()
    {
        // Lógica de morte (desativar objeto, tocar animação de morte, etc.)
        Debug.Log($"{gameObject.name} morreu.");
        Destroy(gameObject);
    }
}
