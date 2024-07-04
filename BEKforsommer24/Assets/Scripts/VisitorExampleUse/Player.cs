using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace VisitorExampleUse {
    public class Player : MonoBehaviour, IVisitorOf<InteractiveObjectVisitor> {
        public List<Item> items = new();
        
        public InteractiveObjectVisitor Visitor { get; }
        
        private Player() {
            Visitor = new PlayerInteractiveObjectVisitor(this);
        }
        
        public void Interact(IInteractableObject interactableObject) {
            interactableObject.Accept(Visitor);
        }
    }
}