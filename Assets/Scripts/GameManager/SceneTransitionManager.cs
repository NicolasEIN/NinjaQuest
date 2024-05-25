using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private List<SceneDataSO> availableScenes; // List of SceneDataSO objects

    public void LoadScene(SceneDataSO sceneDataSO)
    {
        if (sceneDataSO != null)
        {
            Debug.Log("Loading scene: " + sceneDataSO.sceneName);

            // Initiate scene loading
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneDataSO.sceneAssetPath, LoadSceneMode.Single);

            // Find and notify the LoadingScreenUI script
            LoadingScreenUI loadingScreenUI = FindObjectOfType<LoadingScreenUI>();
            if (loadingScreenUI != null)
            {
                loadingScreenUI.StartLoading(asyncOperation);
            }

            // Handle player positioning after scene load
            if (sceneDataSO.spawnPointDataSO != null)
            {
                SpawnPlayer(sceneDataSO.spawnPointDataSO);
            }
        }
    }

    private void SpawnPlayer(SpawnPointDataSO spawnPointDataSO)
    {
        if (spawnPointDataSO != null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Find player by tag
            if (playerObject != null)
            {
                // Set player position based on spawn point data
                Vector3 spawnPosition = spawnPointDataSO.spawnPosition;
                if (spawnPointDataSO.spawnPointObject != null)
                {
                    spawnPosition = spawnPointDataSO.spawnPointObject.transform.position;
                }

                playerObject.transform.position = spawnPosition;
            }
            else
            {
                Debug.LogError("Player object not found with tag 'Player'.");
            }
        }
        else
        {
            Debug.LogError("SpawnPointDataSO is null.");
        }
    }

}
