using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class Interaction : MonoBehaviour
{

    public float interactionRange = 3f; // Alcance da intera��o
    public InputReader inputReader; // Refer�ncia ao InputReader
    public UnityEvent onInteract; // Evento Unity que ser� chamado quando o jogador interagir

    private bool inRange = false; // Verifica se o jogador est� dentro do alcance de intera��o

    void Update()
    {
        // Verifica se o jogador est� dentro do alcance de intera��o e pressionou o bot�o de intera��o definido no InputReader
        if (inRange && inputReader.isInteracting)
        {
            // Chama o evento de intera��o
            onInteract.Invoke();
            inputReader.isInteracting = false; // Reseta o estado de intera��o para evitar chamadas repetidas
        }
    }

    // Verifica se o jogador entrou no alcance de intera��o
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Est� no Range de Intera��o");
        }
    }

    // Verifica se o jogador saiu do alcance de intera��o
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("N�o est� no Range de Intera��o");

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
