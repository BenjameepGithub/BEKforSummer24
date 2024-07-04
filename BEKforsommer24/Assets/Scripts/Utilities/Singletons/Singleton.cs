using UnityEngine;

namespace Utilities.Singletons {
    public class Singleton<T> : MonoBehaviour where T : Component {
        protected static T instance;
        
        public static bool HasInstance => instance != null;
        public static T TryGetInstance() => HasInstance ? instance : null;
        
        public static T Instance {
            get {
                if (instance != null) return instance;
                
                instance = FindAnyObjectByType<T>();
                if (instance != null) return instance;
                
                var go = new GameObject(typeof(T).Name + " - Auto-Generated");
                instance = go.AddComponent<T>();
                return instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need to use awake for something else as well.
        /// </summary>
        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            instance = this as T;
        }
    }
}