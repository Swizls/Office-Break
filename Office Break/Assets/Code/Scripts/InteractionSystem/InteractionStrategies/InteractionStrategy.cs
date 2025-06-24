namespace OfficeBreak.InteractionSystem 
{ 
    public abstract class InteractionStrategy
    {
        protected Interactor Interactor { get; private set; }

        public InteractionStrategy(Interactor interactor)
        {
            Interactor = interactor;
        }

        public abstract void Execute();
    }
}
