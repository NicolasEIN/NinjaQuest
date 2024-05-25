using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class Interaction : MonoBehaviour
{

    public float interactionRange = 3f; // Alcance da interação
    public InputReader inputReader; // Referência ao InputReader
    public UnityEvent onInteract; // Evento Unity que será chamado quando o jogador interagir

    private bool inRange = false; // Verifica se o jogador está dentro do alcance de interação

    void Update()
    {
        // Verifica se o jogador está dentro do alcance de interação e pressionou o botão de interação definido no InputReader
        if (inRange && inputReader.isInteracting)
        {
            // Chama o evento de interação
            onInteract.Invoke();
            inputReader.isInteracting = false; // Reseta o estado de interação para evitar chamadas repetidas
        }
    }

    // Verifica se o jogador entrou no alcance de interação
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Está no Range de Interação");
        }
    }

    // Verifica se o jogador saiu do alcance de interação
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("Não está no Range de Interação");

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
