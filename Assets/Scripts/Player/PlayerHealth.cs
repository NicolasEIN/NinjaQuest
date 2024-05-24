using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float invincibilityTime = 1.5f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    [SerializeField] private GameObject gameOverCanvas;

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // Referências aos componentes ou objetos a serem desativados
    [SerializeField] private MonoBehaviour[] componentsToDisable; // Use MonoBehaviour para scripts e componentes
    [SerializeField] private GameObject[] objectsToDisable; // Use GameObject para objetos inteiros

    // Event to notify when health changes
    public event Action<float> OnHealthChanged;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
        gameOverCanvas.SetActive(false); // Certifique-se de que o Canvas está desativado no início
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
            Debug.Log($"{gameObject.name} Vida atual:  {currentHealth}");

            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }

            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player morreu.");
        gameOverCanvas.SetActive(true); // Ativa o Canvas de Game Over

        // Desativa componentes
        foreach (var component in componentsToDisable)
        {
            component.enabled = false;
        }

        // Desativa objetos
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        // Se você precisar desativar outras funcionalidades do jogo quando o jogador morrer, faça isso aqui.
    }

}
