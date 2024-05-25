using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceManager : MonoBehaviour
{
    private static PersistenceManager instance;

    private void Awake()
    {
        // Se n�o houver uma inst�ncia do PersistenceManager, define esta como a inst�ncia atual e a mant�m entre as cenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se j� houver uma inst�ncia do PersistenceManager, destr�i esta para evitar duplicatas
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se a cena carregada for o menu principal, destrua este objeto de persist�ncia
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }

    // Este m�todo pode ser chamado para garantir que o PersistenceManager seja destru�do manualmente quando necess�rio
    public static void DestroyPersistenceManager()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
    }

    // Este m�todo pode ser chamado para criar novamente o PersistenceManager se ele for destru�do
    public static void CreatePersistenceManager()
    {
        if (instance == null)
        {
            GameObject persistenceManager = new GameObject("PersistenceManager");
            instance = persistenceManager.AddComponent<PersistenceManager>();
            DontDestroyOnLoad(persistenceManager);
        }
    }

}