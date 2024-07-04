using System.Linq;
using UnityEngine;

namespace VisitorExampleUse {
    public class PlayerInteractiveObjectVisitor : InteractiveObjectVisitor {
        private readonly Player _player;
        
        public PlayerInteractiveObjectVisitor(Player player) => _player = player;
        
        public override void Visit(Chest chest) {
            Debug.Log("Player opens the chest!");
            
            foreach (var item in chest.Items.ToList()) {
                chest.Items.Remove(item);
                _player.items.Add(item);
            }
        }

        public override void Visit(Door door) {
            if (door.isOpen) door.Close();
            else door.Open();
        }
    }
}