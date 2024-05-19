using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour, IInteraction
{
    [SerializeField] private InputReader inputReader; // Reference to your InputReader script
    [SerializeField] public float interactionRange;

        public virtual Vector3 GetPlayerPosition()
        {
            // Implement logic to retrieve player's position based on your needs
            // This could involve accessing the player's transform directly
            // or using other methods specific to your player implementation.
            return transform.position;
        }

    public void Interact()
    {
        Debug.Log("Sistema antigo é melhor");
    }

    public void OnInterationEnter(BaseInteraction interactor)
    {
        throw new System.NotImplementedException();
    }

    public void OnInterationExit(BaseInteraction interactor)
    {
        throw new System.NotImplementedException();
    }

    // ... other interaction related methods (optional) ...
}
