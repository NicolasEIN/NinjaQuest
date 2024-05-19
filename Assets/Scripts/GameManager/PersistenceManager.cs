using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceManager : MonoBehaviour
{
    private static bool dataLoaded = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        // Adiciona o PersistenceManager � primeira cena
        GameObject persistenceManager = new GameObject("PersistenceManager");
        persistenceManager.AddComponent<PersistenceManager>();
        DontDestroyOnLoad(persistenceManager);
    }

    private void Awake()
    {
        // Registra um m�todo para ser chamado quando uma nova cena � carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se os dados de persist�ncia ainda n�o foram carregados e a cena n�o � a cena 0
        if (!dataLoaded && scene.buildIndex != 0)
        {
            Debug.Log("Cena inicializada e dados de persist�ncia carregados");
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTDATA")));
            dataLoaded = true; // Marcar como carregado para evitar carregar novamente
        }
    }

    // Certifique-se de desregistrar o evento quando este objeto for destru�do para evitar erros
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}