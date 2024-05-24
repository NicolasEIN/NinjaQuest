using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartsUI : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite threeQuartersHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite quarterHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsParent;
    [SerializeField] private int initialHearts;

    private PlayerHealth playerHealth;
    private List<Image> heartImages = new List<Image>();

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHearts;
            InitializeHearts();
            UpdateHearts(playerHealth.CurrentHealth);
        }
        else
        {
            Debug.LogError("PlayerHealth not found in the scene.");
        }
    }

    private void InitializeHearts()
    {
        for (int i = 0; i < initialHearts; i++)
        {
            AddHeart();
        }
    }

    private void AddHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, heartsParent);
        Image heartImage = newHeart.GetComponent<Image>();
        if (heartImage != null)
        {
            heartImages.Add(heartImage);
            heartImage.sprite = fullHeart; // Set initial heart sprite to full
        }
        else
        {
            Debug.LogError("Heart prefab does not have an Image component.");
        }
    }

    public void AddExtraHeart()
    {
        playerHealth.AddMaxHealth(100f); // Assuming each heart represents 100 health
        AddHeart();
    }

    private void UpdateHearts(float currentHealth)
    {
        float healthPerHeart = playerHealth.MaxHealth / heartImages.Count;

        for (int i = 0; i < heartImages.Count; i++)
        {
            float heartValue = Mathf.Clamp(currentHealth - (i * healthPerHeart), 0, healthPerHeart);
            if (heartValue == healthPerHeart)
            {
                heartImages[i].sprite = fullHeart;
            }
            else if (heartValue >= 0.75f * healthPerHeart)
            {
                heartImages[i].sprite = threeQuartersHeart;
            }
            else if (heartValue >= 0.5f * healthPerHeart)
            {
                heartImages[i].sprite = halfHeart;
            }
            else if (heartValue >= 0.25f * healthPerHeart)
            {
                heartImages[i].sprite = quarterHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHearts;
        }
    }
}
