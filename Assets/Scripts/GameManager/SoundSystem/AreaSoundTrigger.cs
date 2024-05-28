using UnityEngine;

public class AreaSoundTrigger : MonoBehaviour
{
    public string areaSoundName; // Nome do som espec�fico para a �rea
    private ISoundManager soundManager;

    void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que entrou na �rea � o jogador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entrou na �rea!");
            // Toca o som espec�fico da �rea
            soundManager.PlaySound(areaSoundName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verifique se o objeto que saiu da �rea � o jogador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Saiu da �rea!");
            // Restaura o som da cena
            soundManager.RestoreSceneMusic();
        }
    }
}
