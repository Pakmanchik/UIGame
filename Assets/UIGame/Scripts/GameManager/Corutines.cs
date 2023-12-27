using System.Collections;
using UnityEngine;

namespace UIGame.Scripts.GameManager
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines Instance
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
            return Instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine routine)
        {
            if (routine != null)
                Instance.StopCoroutine(routine);
        }
    }
}