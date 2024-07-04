using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.RuntimeSet {
    public abstract class GenericRuntimeSetScrub<T> : ScriptableObject {
        public List<T> items = new ();
        
        public event Action<int> OnCountChanged;
        
        public void Add(T item) {
            if (!items.Contains(item)) items.Add(item);
            OnCountChanged?.Invoke(items.Count);
        }
        
        public void Remove(T item) {
            if (items.Contains(item)) items.Remove(item);
            OnCountChanged?.Invoke(items.Count);
        }
    }
}