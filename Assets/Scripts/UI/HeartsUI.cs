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
        // Calcular o n�mero de cora��es necess�rios com base na vida m�xima inicial do jogador
        int maxHearts = Mathf.CeilToInt(playerHealth.MaxHealth / 100f); // Assumindo que cada cora��o representa 100 de vida

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
            heartImage.sprite = fullHeart; // Definir o sprite inicial do cora��o como cheio
        }
        else
        {
            Debug.LogError("Heart prefab does not have an Image component.");
        }
    }

    private void UpdateHearts(float currentHealth)
    {
        // Calcular o n�mero de cora��es necess�rios com base na vida atual do jogador
        int maxHearts = Mathf.CeilToInt(currentHealth / 100f); // Assumindo que cada cora��o representa 100 de vida

        // Remover cora��es extras se houver mais do que o necess�rio
        while (heartImages.Count > maxHearts)
        {
            Destroy(heartImages[heartImages.Count - 1].gameObject);
            heartImages.RemoveAt(heartImages.Count - 1);
        }

        // Adicionar cora��es extras se n�o houver o suficiente
        while (heartImages.Count < maxHearts)
        {
            AddHeart();
        }

        // Atualizar os sprites dos cora��es restantes
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
