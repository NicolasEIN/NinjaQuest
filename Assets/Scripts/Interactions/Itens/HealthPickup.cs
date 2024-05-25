using UnityEngine;

public class HealthPickup : MonoBehaviour
{
 
    private void Start()
    {
        Interact interact = GetComponent<Interact>();
        if (interact != null)
        {
            interact.OnInteract.AddListener(HandleInteraction);
        }
    }

    private void HandleInteraction()
    {
        Debug.Log("Health Pickup Used"); // Verifica se o HealthPickup está respondendo corretamente à interação
        // Lógica para curar o jogador
    }
}
