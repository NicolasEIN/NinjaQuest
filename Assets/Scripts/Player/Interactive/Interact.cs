using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent OnInteract = new UnityEvent();  // UnityEvent to handle interactions

    public void InteractWithObject()
    {
        Debug.Log("Object Interacted"); // Verifica se a interação está sendo detectada corretamente
        OnInteract?.Invoke();  // Invoke the interaction event
    }
}
