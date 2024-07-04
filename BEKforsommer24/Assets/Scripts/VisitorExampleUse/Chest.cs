using System.Collections.Generic;
using UnityEngine;

namespace VisitorExampleUse {
    public class Chest : MonoBehaviour, IInteractableObject {
        public readonly List<Item> Items = new();
    }
    
    public class Door : MonoBehaviour, IInteractableObject {
        public bool isOpen;

        public void Close() {
            
        }

        public void Open() {
            
        }
    }
}