using Utilities;

namespace VisitorExampleUse {
    public interface IInteractableObject : IVisitable<IInteractableObject, InteractiveObjectVisitor> {
    }
    
    public abstract class InteractiveObjectVisitor : Visitor<IInteractableObject> {
        public abstract void Visit(Chest chest);
        public abstract void Visit(Door door);
    }
}