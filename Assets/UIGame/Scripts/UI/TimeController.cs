using System;
using System.Collections;
using UIGame.Scripts.GameManager;
using UnityEngine;

namespace UIGame.Scripts.UI
{
    public class TimeController
    {
        public event Action<int> onTimeNow;
        public event Action onTimeExpire;

        private int _timeNow;
        private Coroutine _routine;
        
        public void StartTimer(int time)
        {
            _timeNow = time;
            
           onTimeNow?.Invoke(_timeNow);
            
            _routine = Coroutines.StartRoutine(BeginTimer());
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
            Coroutines.StopRoutine(_routine);
        }
    }
}