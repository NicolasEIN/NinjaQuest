using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas; // Reference to the loading screen canvas
    [SerializeField] private Text txtPercentage; // Text UI element for displaying loading percentage
    [SerializeField] private Slider loadingBar; // Slider UI element for loading progress

    private AsyncOperation asyncOperation; // Reference to the asynchronous scene loading operation
    private float progressDelay = 0.5f; // Delay for keeping the loading screen visible

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
        loadingCanvas.enabled = false; // Deactivate loading canvas initially
    }

    public void StartLoading(AsyncOperation op)
    {
        if (op != null)
        {
            // Activate loading canvas during scene transition
            loadingCanvas.enabled = true;

            // Assign the async operation and set up progress update
            asyncOperation = op;
            StartCoroutine(UpdateLoadingProgressWithDelay(asyncOperation));
        }
    }

    private IEnumerator UpdateLoadingProgressWithDelay(AsyncOperation op)
    {
        while (op != null && !op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f); // Normalize progress (0 to 1)

            // Update UI elements with progress value
            loadingBar.value = progress;
            if (txtPercentage != null)
            {
                txtPercentage.text = (int)(progress * 100) + "%"; // Display percentage as an integer
            }

            // Yield for a delay before updating progress again
            yield return new WaitForSeconds(progressDelay);
        }

        // Keep the loading screen visible until progress reaches 100%
        while (loadingBar.value < 1f)
        {
            loadingBar.value += Time.deltaTime / progressDelay; // Increment progress smoothly
            if (txtPercentage != null)
            {
                txtPercentage.text = (int)(loadingBar.value * 100) + "%"; // Update percentage display
            }

            yield return null; // Wait for next frame
        }

        // Deactivate loading canvas and start scene after delay
        yield return new WaitForSeconds(progressDelay);
        loadingCanvas.enabled = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Scene has loaded, no need to keep the loading screen visible
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event on script destruction
    }
}

