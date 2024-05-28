using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas; // Refer�ncia para o canvas da tela de loading
    [SerializeField] private Text txtPercentage; // Elemento de UI de texto para exibir a porcentagem de loading
    [SerializeField] private Slider loadingBar; // Elemento de UI de slider para o progresso do loading

    private AsyncOperation asyncOperation; // Refer�ncia para a opera��o de loading de cena ass�ncrona
    private float progressDelay = 0.5f; // Delay para manter a tela de loading vis�vel

    private void Awake()
    {
        loadingCanvas.enabled = false; // Desativa o canvas de loading inicialmente
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Se inscreve para o evento de cena carregada
        loadingCanvas.enabled = false; // Desativa o canvas de loading inicialmente
    }

    public void StartLoading(AsyncOperation op)
    {
        if (op != null)
        {
            // Ativa o canvas de loading durante a transi��o de cena
            loadingCanvas.enabled = true;

            // Atribui a opera��o ass�ncrona e configura a atualiza��o de progresso
            asyncOperation = op;
            StartCoroutine(UpdateLoadingProgressWithDelay(asyncOperation));
        }
    }

    public void StartGame()
    {
        StartLoading(SceneManager.LoadSceneAsync(1)); // Substitua "NomeDaCenaDoJogo" pelo nome da sua cena do jogo
    }

    private IEnumerator UpdateLoadingProgressWithDelay(AsyncOperation op)
    {
        while (op != null && !op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f); // Normaliza o progresso (0 a 1)

            // Atualiza os elementos de UI com o valor de progresso
            loadingBar.value = progress;
            if (txtPercentage != null)
            {
                txtPercentage.text = (int)(progress * 100) + "%"; // Exibe a porcentagem como um n�mero inteiro
            }

            // Aguarda um delay antes de atualizar o progresso novamente
            yield return new WaitForSeconds(progressDelay);
        }

        // Mant�m a tela de loading vis�vel at� que o progresso atinja 100%
        while (loadingBar.value < 1f)
        {
            loadingBar.value += Time.deltaTime / progressDelay; // Incrementa o progresso suavemente
            if (txtPercentage != null)
            {
                txtPercentage.text = (int)(loadingBar.value * 100) + "%"; // Atualiza a exibi��o da porcentagem
            }

            yield return null; // Aguarda o pr�ximo frame
        }

        // Desativa o canvas de loading e inicia a cena ap�s um delay
        yield return new WaitForSeconds(progressDelay);
        loadingCanvas.enabled = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // A cena foi carregada, n�o h� necessidade de manter a tela de loading vis�vel
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Se desinscreve do evento de cena carregada quando o script � destru�do
    }

}

