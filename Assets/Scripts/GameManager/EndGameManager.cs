using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Refer�ncia ao canvas de fim de jogo
    public SoundManager soundManager; // Refer�ncia ao SoundManager (opcional, para tocar uma m�sica de fim de jogo)

    private void Start()
    {
        // Certifique-se de que o canvas de fim de jogo esteja desativado no in�cio
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        // Encontre o SoundManager na cena
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void TriggerGameOver()
    {
        // Ativa o canvas de fim de jogo
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Pausa o jogo
        Time.timeScale = 0f;

        // Toca a m�sica de fim de jogo, se aplic�vel
        if (soundManager != null)
        {
            soundManager.StopMusic();
            soundManager.PlayEndGameSound();
        }
    }
}
