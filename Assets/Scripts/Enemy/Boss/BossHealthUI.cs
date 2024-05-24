using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private BossHealth bossHealth;

    private void Start()
    {
        if (bossHealth != null)
        {
            bossHealth.OnHealthChanged += UpdateHealthBar;
            InitializeHealthBar();
        }
        else
        {
            Debug.LogError("bossHealth reference is missing.");
        }
    }

    private void InitializeHealthBar()
    {
        healthSlider.maxValue = bossHealth.MaxHealth;
        healthSlider.value = bossHealth.CurrentHealth;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    private void OnDestroy()
    {
        if (bossHealth != null)
        {
            bossHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
