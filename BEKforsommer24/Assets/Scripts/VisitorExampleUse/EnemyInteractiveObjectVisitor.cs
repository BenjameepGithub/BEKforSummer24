using UnityEngine;
using Utilities.ExtensionMethods;

namespace VisitorExampleUse {
    public class EnemyInteractiveObjectVisitor : InteractiveObjectVisitor {
        private readonly Enemy _enemy;
        
        public EnemyInteractiveObjectVisitor(Enemy enemy) => _enemy = enemy;
        
        public override void Visit(Chest chest) {
            Debug.Log("NPC interacts with the chest!");
            
            chest.Items.Add(_enemy.items.GetRandom());
        }

        public override void Visit(Door door) {
            if (door.isOpen) door.Close();
        }
    }
}