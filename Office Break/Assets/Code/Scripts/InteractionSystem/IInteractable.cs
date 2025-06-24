namespace OfficeBreak.InteractionSystem
{
    public interface IInteractable
    {
        public InteractionStrategy Interact(Interactor interactor);
    }
}
