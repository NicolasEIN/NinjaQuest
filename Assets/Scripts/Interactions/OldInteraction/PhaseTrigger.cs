using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseTrigger : MonoBehaviour
{
    [Header("Door Settings")]
    public SceneDataSO sceneToLoad; // Reference to the SceneDataSO object representing the scene to load
    public bool requireKey;
    public GameObject keyObject;

    private bool doorOpen = false; // Flag indicating if the door is open

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }


    private void OpenDoor()
    {
        doorOpen = true;

        // Check if sceneToLoad is assigned before loading
        if (sceneToLoad != null)
        {
            // Load the scene using SceneTransitionManager
            SceneTransitionManager sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
            // Assuming SceneTransitionManager is in the scene
            if (sceneTransitionManager != null)
            {
                sceneTransitionManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("SceneTransitionManager not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("sceneToLoad is not assigned in DoorInteraction script.");
        }
    }

    // Add a method to close the door if necessary (optional)
    public void CloseDoor()
    {
        doorOpen = false;
        // Implement logic to visually close the door (animation, physics, etc.)
    }

}