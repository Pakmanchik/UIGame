using System.Collections;
using UnityEngine;

namespace UIGame.Scripts.GameManager.Coroutines
{
    public interface ICoroutineManager
    {
        public void Stop(Coroutine routine);
        public Coroutine Start(IEnumerator enumerator);
        public void StopAll();
    }
}