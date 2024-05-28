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
        // Calcular o número de corações necessários com base na vida máxima inicial do jogador
        int maxHearts = Mathf.CeilToInt(playerHealth.MaxHealth / 100f); // Assumindo que cada coração representa 100 de vida

        for (int i = 0; i < maxHearts; i++)
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
            heartImage.sprite = fullHeart; // Definir o sprite inicial do coração como cheio
        }
        else
        {
            Debug.LogError("Heart prefab does not have an Image component.");
        }
    }

    private void UpdateHearts(float currentHealth)
    {
        // Calcular o número de corações necessários com base na vida atual do jogador
        int maxHearts = Mathf.CeilToInt(currentHealth / 100f); // Assumindo que cada coração representa 100 de vida

        // Remover corações extras se houver mais do que o necessário
        while (heartImages.Count > maxHearts)
        {
            Destroy(heartImages[heartImages.Count - 1].gameObject);
            heartImages.RemoveAt(heartImages.Count - 1);
        }

        // Adicionar corações extras se não houver o suficiente
        while (heartImages.Count < maxHearts)
        {
            AddHeart();
        }

        // Atualizar os sprites dos corações restantes
        for (int i = 0; i < heartImages.Count; i++)
        {
            float heartValue = currentHealth - (i * 100f);
            if (heartValue >= 100f)
            {
                heartImages[i].sprite = fullHeart;
            }
            else if (heartValue >= 75f)
            {
                heartImages[i].sprite = threeQuartersHeart;
            }
            else if (heartValue >= 50f)
            {
                heartImages[i].sprite = halfHeart;
            }
            else if (heartValue >= 25f)
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
