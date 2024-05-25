using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    public SceneDataSO sceneToLoad; // Referência ao objeto SceneDataSO que representa a cena a ser carregada
    public bool requireKey;
    public GameObject keyObject;

    private bool doorOpen = false; // Flag indicando se a porta está aberta

    public void Interact()
    {
        if (doorOpen) return; // Evita abrir a porta novamente se já estiver aberta

        // Se uma chave é necessária e o jogador não a possui, não abra a porta
        if (requireKey && keyObject == null)
        {
            Debug.Log("A chave é necessária para abrir esta porta.");
            return;
        }

        doorOpen = true;

        // Verifica se sceneToLoad está atribuído antes de carregar
        if (sceneToLoad != null)
        {
            // Carrega a cena usando SceneTransitionManager
            SceneTransitionManager sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
            if (sceneTransitionManager != null)
            {
                sceneTransitionManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("SceneTransitionManager não encontrado na cena.");
            }
        }
        else
        {
            Debug.LogError("sceneToLoad não está atribuído no script DoorInteraction.");
        }
    }

    // Adiciona um método para fechar a porta, se necessário (opcional)
    public void CloseDoor()
    {
        doorOpen = false;
        // Implementar lógica para fechar visualmente a porta (animação, física, etc.)
    }
}
