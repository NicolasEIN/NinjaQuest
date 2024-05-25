using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public PersistenceManager persistenceManager; // Adicione uma referência ao PersistenceManager

    private void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>(); // Encontre o PersistenceManager
    }

    public void ResetGame()
    {
        // Certifique-se de destruir o PersistenceManager antes de reiniciar o jogo
        PersistenceManager.DestroyPersistenceManager();

        // Carrega a cena com o índice desejado
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        // Destrua o PersistenceManager para garantir que os objetos persistentes sejam limpos
        PersistenceManager.DestroyPersistenceManager();

        // Volte para o menu principal
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
