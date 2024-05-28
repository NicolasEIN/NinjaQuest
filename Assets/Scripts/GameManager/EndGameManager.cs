using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Referência ao canvas de fim de jogo
    public SoundManager soundManager; // Referência ao SoundManager (opcional, para tocar uma música de fim de jogo)

    private void Start()
    {
        // Certifique-se de que o canvas de fim de jogo esteja desativado no início
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

        // Toca a música de fim de jogo, se aplicável
        if (soundManager != null)
        {
            soundManager.StopMusic();
            soundManager.PlayEndGameSound();
        }
    }
}
