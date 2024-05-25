using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceManager : MonoBehaviour
{
    private static PersistenceManager instance;

    private void Awake()
    {
        // Se não houver uma instância do PersistenceManager, define esta como a instância atual e a mantém entre as cenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já houver uma instância do PersistenceManager, destrói esta para evitar duplicatas
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se a cena carregada for o menu principal, destrua este objeto de persistência
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }

    // Este método pode ser chamado para garantir que o PersistenceManager seja destruído manualmente quando necessário
    public static void DestroyPersistenceManager()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
    }

    // Este método pode ser chamado para criar novamente o PersistenceManager se ele for destruído
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