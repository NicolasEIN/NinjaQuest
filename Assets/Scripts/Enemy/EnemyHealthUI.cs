using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private EnemyHealth enemyHealth;

    private void Start()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged += UpdateHealthBar;
            InitializeHealthBar();
        }
        else
        {
            Debug.LogError("EnemyHealth reference is missing.");
        }
    }

    private void InitializeHealthBar()
    {
        healthSlider.maxValue = enemyHealth.MaxHealth;
        healthSlider.value = enemyHealth.CurrentHealth;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    private void OnDestroy()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
