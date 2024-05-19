using UnityEngine;

public class BaseInteraction : MonoBehaviour, IInteraction
{
    [SerializeField] private float interactionRange = 2f; // Interaction range
    [SerializeField] private LayerMask interactionMask; // Layer mask for interaction (optional)

    private IInteraction interaction;

    protected bool isInRange = false; // Flag indicating if player is in interaction range

    private PlayerInteraction playerInteraction;

    //void Start()
    //{
    //    playerInteraction = FindObjectOfType<PlayerInteraction>();
    //    if (playerInteraction == null)
    //    {
    //        Debug.LogError("PlayerInteraction script not found in the scene!");
    //    }
    //}

    private void Update()
    {
        if (interaction != null)
        {
            // Check for interaction using Physics overlap with optional layer mask
            Collider[] collidersInRange = Physics.OverlapSphere(transform.position, interactionRange, interactionMask);
            bool playerInRange = false;

            for (int i = 0; i < collidersInRange.Length; i++)
            {
                if (collidersInRange[i].gameObject == playerInteraction.gameObject)
                {
                    playerInRange = true;
                    break;
                }
            }

            isInRange = playerInRange;
        }
        else
        {
            isInRange = false; // Reset flag if interaction is null
        }

        // **Interaction logic now relies on IInteraction interface:**
        if (isInRange)
        {
            Interact(); // Call Interact() based on the interface implementation
            UpdateInteractionPromptUI();
        }
    }

    private void UpdateInteractionPromptUI()
    {
        // Implement logic to update interaction prompt UI based on 'isInRange'

        Debug.Log("Aperta E para Interagir"); // Or update your existing prompt logic
    }

    public virtual void Interact()
    {
        // Implement specific interaction logic for each interactable object
        Debug.Log("Interacting with " + gameObject.name);
    }

    public void OnInterationEnter(BaseInteraction interactor)
    {

    }

    public void OnInterationExit(BaseInteraction interactor)
    {

    }
}