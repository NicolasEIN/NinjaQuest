using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    public SceneDataSO sceneToLoad; // Refer�ncia ao objeto SceneDataSO que representa a cena a ser carregada
    public bool requireKey;
    public GameObject keyObject;

    private bool doorOpen = false; // Flag indicando se a porta est� aberta

    public void Interact()
    {
        if (doorOpen) return; // Evita abrir a porta novamente se j� estiver aberta

        // Se uma chave � necess�ria e o jogador n�o a possui, n�o abra a porta
        if (requireKey && keyObject == null)
        {
            Debug.Log("A chave � necess�ria para abrir esta porta.");
            return;
        }

        doorOpen = true;

        // Verifica se sceneToLoad est� atribu�do antes de carregar
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
                Debug.LogError("SceneTransitionManager n�o encontrado na cena.");
            }
        }
        else
        {
            Debug.LogError("sceneToLoad n�o est� atribu�do no script DoorInteraction.");
        }
    }

    // Adiciona um m�todo para fechar a porta, se necess�rio (opcional)
    public void CloseDoor()
    {
        doorOpen = false;
        // Implementar l�gica para fechar visualmente a porta (anima��o, f�sica, etc.)
    }
}
