using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas; // Referência para o canvas da tela de loading
    [SerializeField] private Text txtPercentage; // Elemento de UI de texto para exibir a porcentagem de loading
    [SerializeField] private Slider loadingBar; // Elemento de UI de slider para o progresso do loading

    private AsyncOperation asyncOperation; // Referência para a operação de loading de cena assíncrona
    private float progressDelay = 0.5f; // Delay para manter a tela de loading visível

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Se inscreve para o evento de cena carregada
        loadingCanvas.enabled = false; // Desativa o canvas de loading inicialmente
    }

    public void StartLoading(AsyncOperation op)
    {
        if (op != null)
        {
            // Ativa o canvas de loading durante a transição de cena
            loadingCanvas.enabled = true;

            // Atribui a operação assíncrona e configura a atualização de progresso
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
                txtPercentage.text = (int)(progress * 100) + "%"; // Exibe a porcentagem como um número inteiro
            }

            // Aguarda um delay antes de atualizar o progresso novamente
            yield return new WaitForSeconds(progressDelay);
        }

        // Mantém a tela de loading visível até que o progresso atinja 100%
        while (loadingBar.value < 1f)
        {
            loadingBar.value += Time.deltaTime / progressDelay; // Incrementa o progresso suavemente
            if (txtPercentage != null)
            {
                txtPercentage.text = (int)(loadingBar.value * 100) + "%"; // Atualiza a exibição da porcentagem
            }

            yield return null; // Aguarda o próximo frame
        }

        // Desativa o canvas de loading e inicia a cena após um delay
        yield return new WaitForSeconds(progressDelay);
        loadingCanvas.enabled = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // A cena foi carregada, não há necessidade de manter a tela de loading visível
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Se desinscreve do evento de cena carregada quando o script é destruído
    }

//    [SerializeField] private Canvas loadingCanvas; // Reference to the loading screen canvas
//    [SerializeField] private Text txtPercentage; // Text UI element for displaying loading percentage
//    [SerializeField] private Slider loadingBar; // Slider UI element for loading progress

//    private AsyncOperation asyncOperation; // Reference to the asynchronous scene loading operation
//    private float progressDelay = 0.5f; // Delay for keeping the loading screen visible

//    void Start()
//    {
//        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
//        loadingCanvas.enabled = false; // Deactivate loading canvas initially
//    }

//    public void StartLoading(AsyncOperation op)
//    {
//        if (op != null)
//        {
//            // Activate loading canvas during scene transition
//            loadingCanvas.enabled = true;

//            // Assign the async operation and set up progress update
//            asyncOperation = op;
//            StartCoroutine(UpdateLoadingProgressWithDelay(asyncOperation));
//        }
//    }

//    private IEnumerator UpdateLoadingProgressWithDelay(AsyncOperation op)
//    {
//        while (op != null && !op.isDone)
//        {
//            float progress = Mathf.Clamp01(op.progress / 0.9f); // Normalize progress (0 to 1)

//            // Update UI elements with progress value
//            loadingBar.value = progress;
//            if (txtPercentage != null)
//            {
//                txtPercentage.text = (int)(progress * 100) + "%"; // Display percentage as an integer
//            }

//            // Yield for a delay before updating progress again
//            yield return new WaitForSeconds(progressDelay);
//        }

//        // Keep the loading screen visible until progress reaches 100%
//        while (loadingBar.value < 1f)
//        {
//            loadingBar.value += Time.deltaTime / progressDelay; // Increment progress smoothly
//            if (txtPercentage != null)
//            {
//                txtPercentage.text = (int)(loadingBar.value * 100) + "%"; // Update percentage display
//            }

//            yield return null; // Wait for next frame
//        }

//        // Deactivate loading canvas and start scene after delay
//        yield return new WaitForSeconds(progressDelay);
//        loadingCanvas.enabled = false;
//    }

//    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        // Scene has loaded, no need to keep the loading screen visible
//    }

//    void OnDestroy()
//    {
//        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event on script destruction
//    }
}

