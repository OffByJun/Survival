namespace AstraNope.Data.Entities
{
    public interface IConditionalInteractable : IInteractable
    {
        bool CanInteract();
        string RequirementLabel();
    }

    public interface IConditionalInteractionTarget : IConditionalInteractable, IInteractionTarget { }
}
