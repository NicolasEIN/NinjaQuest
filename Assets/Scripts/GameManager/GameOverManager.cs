using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("HomeTown");
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu"); // Substitua "MainMenu" pelo nome da sua cena do menu principal
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
