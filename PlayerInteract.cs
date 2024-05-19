using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
   [SerializeField] private InputReader inputReader;

    void Start()
    {
        if (inputReader == null)
        {
            Debug.LogError("No InputReader assigned to PlayerInteractions!");
            return;
        }

        // Subscribe to InteractionEvent from all interactable objects
        foreach (IInteractable interactable in FindObjectsOfType<IInteractable>())
        {
            interactable.OnInteractionEvent.AddListener(OnInteractionReceived);
        }
    }

    private void OnInteractionReceived()
    {
        // Handle interaction based on the interactable object (if needed)
        // You can access the interactable object through the event sender
        var interactableObject = (IInteractive)EventSystem.current.currentSelectedGameObject;

        // Perform interaction logic using Unity Events (replace with your actions)
        Debug.Log("Interacted with " + interactableObject.gameObject.name);

        // Example: Play an animation
        if (interactableObject.gameObject.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetTrigger("Interact");
        }

        // Example: Play a sound effect
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
