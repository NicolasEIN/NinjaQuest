using UnityEngine;

public class AreaSoundTrigger : MonoBehaviour
{
    public string areaSoundName; // Nome do som específico para a área
    private ISoundManager soundManager;

    void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que entrou na área é o jogador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entrou na área!");
            // Toca o som específico da área
            soundManager.PlaySound(areaSoundName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verifique se o objeto que saiu da área é o jogador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Saiu da área!");
            // Restaura o som da cena
            soundManager.RestoreSceneMusic();
        }
    }
}
