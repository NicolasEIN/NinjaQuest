using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AsyncOperation = UnityEngine.AsyncOperation;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private ISoundManager soundManager;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text txtPercentage;
    [SerializeField] private SceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        loadingCanvas.enabled = false;
        // Certifique-se de que o SoundManager seja obtido corretamente
        if (soundManager == null)
        {
            Debug.LogError("SoundManager não está atribuído ao SceneLoader.");
        }
    }

    public void StartLoading(SceneDataSO sceneDataSO)
    {
        StartCoroutine(TransitionToLoading(sceneDataSO));
    }

    private IEnumerator TransitionToLoading(SceneDataSO sceneDataSO)
    {
        // Toca o som de clique do botão
        soundManager.PlaySound("ButtonClick");

        menuCanvas.enabled = false;
        loadingCanvas.enabled = true;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneDataSO.sceneAssetPath, LoadSceneMode.Single);

        // Aguarda até que a nova cena esteja completamente carregada
        while (!asyncLoad.isDone)
        {
            // Calcula a porcentagem de carregamento da cena
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Atualiza a barra de progresso da tela de loading
            loadingBar.value = progress;

            // Atualiza o texto de porcentagem da tela de loading
            txtPercentage.text = Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }

        // Toca a música específica da nova cena
        soundManager.CrossfadeMusic(sceneDataSO.sceneName);

        // Aguarda um pequeno intervalo para garantir uma transição suave de som
        yield return new WaitForSeconds(soundManager.GetCrossfadeDuration());

        // Desativa o canvas de loading após o carregamento completo
        loadingCanvas.enabled = false;

        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
