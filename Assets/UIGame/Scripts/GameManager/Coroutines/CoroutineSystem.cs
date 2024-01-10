using System.Collections;
using UnityEngine;

namespace UIGame.Scripts.GameManager.Coroutines
{
    public class CoroutineSystem : ICoroutineManager
    {
        public Coroutine Start(IEnumerator enumerator)
        {
            return BasicCoroutine.StartCoroutines(enumerator);
        }

        public void Stop(Coroutine routine)
        {
            if (routine != null)
                BasicCoroutine.StopCoroutines(routine);
        }

        public void StopAll()
        {
            BasicCoroutine.StopAll();
        }
    }
}