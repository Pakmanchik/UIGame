using System;
using System.Collections;
using UIGame.Scripts.GameManager.Coroutines;
using UnityEngine;

namespace UIGame.Scripts.UI
{
    public class TimeController
    {
        public TimeController(ICoroutineManager coroutineManager)
        {
            _coroutineManager = coroutineManager;
        }
        
        public event Action<int> onTimeNow;
        public event Action onTimeExpire;

        private int _timeNow;
        private Coroutine _routine;

        private readonly ICoroutineManager _coroutineManager;
        
        
        public void StartTimer(int time)
        {
            _timeNow = time;
            
           onTimeNow?.Invoke(_timeNow);
            
            _routine = _coroutineManager.Start(BeginTimer());
        }

        private IEnumerator BeginTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
                _timeNow -= 1;
                onTimeNow?.Invoke(_timeNow);

                if (_timeNow <= 0)
                {
                    StopTimer();
                    onTimeExpire?.Invoke();
                    break;
                }
            }
        }

        public void StopTimer()
        {
            _coroutineManager.Stop(_routine);
        }
    }
}