using System.Collections;
using UnityEngine;

namespace SmallBallBigPlane.Helpers
{
    public class CoroutinesHandler : MonoBehaviour
    {
        private static CoroutinesHandler _instance;

        private static CoroutinesHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[COROUTINES MANAGER]");
                    _instance = go.AddComponent<CoroutinesHandler>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        public static UnityEngine.Coroutine StartRoutine(IEnumerator enumerator) => Instance.StartCoroutine(enumerator);

        public static bool StopRoutine(UnityEngine.Coroutine coroutine)
        {
            if (coroutine == null)
                return false;

            Instance.StopCoroutine(coroutine);
            return true;
        }
    }
}