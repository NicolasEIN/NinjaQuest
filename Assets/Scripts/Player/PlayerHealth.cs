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
    [SerializeField] private Animator animator; // Adicionado para controlar a animação
    [SerializeField] private SoundManager soundManager;

    // Event to notify when health changes
    public event Action<float> OnHealthChanged;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
        SetGameOverCanvasActive(false); // Certifique-se de que o Canvas está desativado no início
        soundManager = SoundManager.Instance;
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
            Debug.Log($"{gameObject.name} Vida atual: {currentHealth}");
            soundManager.PlayDamageSound();

            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                Die(); // Chama o método Die imediatamente quando a saúde atingir zero
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
        Debug.Log($"{gameObject.name} foi curado. Vida atual: {currentHealth}");
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth; // Atualiza a vida atual para o novo máximo
        Debug.Log($"{gameObject.name} aumentou a vida máxima. Vida atual e máxima: {maxHealth}");
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player morreu.");
        SetGameOverCanvasActive(true); // Ativa o Canvas de Game Over
        PauseGame(); // Pausa o jogo
    }

    private void SetGameOverCanvasActive(bool active)
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(active);

            if (active)
            {
                soundManager.StopMusic();
                soundManager.PlayGameOverSound();
            }
            else
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                soundManager.PlayMusic(currentSceneName);
            }
        }
        else
        {
            Debug.LogWarning("O GameObject do canvas de game over não está atribuído.");
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Define a escala de tempo para zero, pausando o jogo
    }

}
