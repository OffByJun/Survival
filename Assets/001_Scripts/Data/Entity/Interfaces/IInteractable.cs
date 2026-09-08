namespace AstraNope.Data.Entities
{
    public interface IInteractable
    {
        void Interact();
    }

    public interface IInteractionTarget : IInteractable, IInteractableInfo { }
}
