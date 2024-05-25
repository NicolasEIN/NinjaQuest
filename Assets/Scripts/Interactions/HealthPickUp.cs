using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
       [SerializeField] private float range;
    [SerializeField] private float healthAmount; // Quantidade de vida que este item restaura
    [SerializeField] private float maxHealthIncrease; // Quantidade de vida máxima que este item aumenta se a vida estiver cheia

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth.CurrentHealth == playerHealth.MaxHealth)
                {
                    // Aumenta a vida máxima do jogador
                    playerHealth.AddMaxHealth(maxHealthIncrease);
                    Debug.Log($"Você aumentou a vida máxima em {maxHealthIncrease}");
                }
                else
                {
                    // Cura o jogador
                    playerHealth.Heal(healthAmount);
                    Debug.Log($"Você curou {healthAmount}");
                }
                OnPickedUp();
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the Player object.");
            }
        }
    }

    private void OnPickedUp()
    {
        Debug.Log("Pegou a vida");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
