using System.Collections;
using UnityEngine;

namespace UIGame.Scripts.GameManager
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines instance
        {
            get
            {
                if (_instance != null) return _instance;

                var cm = new GameObject("[COROUTINE MANAGER]");
                _instance = cm.AddComponent<Coroutines>();
                DontDestroyOnLoad(cm);

                return _instance;
            }
        }

        private static Coroutines _instance;

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine routine)
        {
            if (routine != null)
                instance.StopCoroutine(routine);
        }
    }
}