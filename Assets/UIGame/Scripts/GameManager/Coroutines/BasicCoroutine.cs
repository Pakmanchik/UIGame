using System.Collections;
using UnityEngine;

namespace UIGame.Scripts.GameManager.Coroutines
{
    
    // Моей фантазии хватиило только на декоратор, который в себе уже имеет интерфейс
    // Может завтра на холодную голову получится 
    
    public sealed class BasicCoroutine : MonoBehaviour
    {
        private static BasicCoroutine InstanceCoroutine
        {
            get
            {
                if (_instanceCoroutine != null) return _instanceCoroutine;

                var cm = new GameObject("[COROUTINE MANAGER]");
                _instanceCoroutine = cm.AddComponent<BasicCoroutine>();
                DontDestroyOnLoad(cm);
                return _instanceCoroutine;
            }
        }

        private static BasicCoroutine _instanceCoroutine;

        public static Coroutine StartCoroutines(IEnumerator enumerator)
        {
            return InstanceCoroutine.StartCoroutine(enumerator);
        }

        public static void StopCoroutines(Coroutine routine)
        {
            if (routine != null)
                InstanceCoroutine.StopCoroutine(routine);
        }

        public static void StopAll()
        {
            InstanceCoroutine.StopAllCoroutines();
        }
    }
}