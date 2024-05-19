public interface IInteraction
{
    void OnInterationEnter(BaseInteraction interactor);

    void OnInterationExit(BaseInteraction interactor);

    void Interact();
}