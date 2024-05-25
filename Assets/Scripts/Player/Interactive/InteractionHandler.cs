using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public InputReader inputReader;
    public float interactionRange = 2.0f;

    private void Update()
    {
        if (inputReader == null) return;

        DetectInteractable();
    }

    private void DetectInteractable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (var hitCollider in hitColliders)
        {
            Interact interact = hitCollider.GetComponent<Interact>();
            if (interact != null)
            {
                interact.InteractWithObject();
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
