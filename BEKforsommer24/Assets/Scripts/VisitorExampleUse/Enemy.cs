using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace VisitorExampleUse {
    public class Enemy : MonoBehaviour, IVisitorOf<InteractiveObjectVisitor> {
        public List<Item> items = new();
        
        public InteractiveObjectVisitor Visitor { get; }
        
        private Enemy() {
            Visitor = new EnemyInteractiveObjectVisitor(this);
        }
    }
}