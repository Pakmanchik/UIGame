using System;
using UIGame.Scripts.Back;
using UIGame.Scripts.Settings;
using UnityEngine;

namespace UIGame.Scripts.Model
{
    public interface IBackModel
    {
        public event Action<Sprite> onCheckAnswer; 
        public event Action onNextHint; 
        public event Action onExpireTime; 
        
        public void SetDataLevel(ImportantDataLevel importantDataLevel);
        public void SetNewHint(int countHint, string hintText);
        public void SetResultAnswer(AnswerResult resultAnswer);
        public void SetSpriteForOptionalAnswer(Sprite sprite);
        public void GameFinal(int valueCorrectAnswer, int valueQuestion);
    }
}