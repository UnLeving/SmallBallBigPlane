using UnityEngine;

namespace Helpers
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning($"[Singleton] Destroying duplicate instance of {typeof(T)}");
                
                Destroy(gameObject);
            }
        }
    }
}