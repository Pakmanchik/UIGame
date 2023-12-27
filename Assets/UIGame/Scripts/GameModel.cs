using System;
using UIGame.Scripts.Back;
using UnityEngine;

namespace UIGame.Scripts
{
    public class GameModel : IBackModel, IUIModel
    {
// Back          
        public event Action onNextHint; 
        public event Action<Sprite> onCheckAnswer; 
        public event Action onExpireTime; 
        
// UI        
        public event Action<ImportantDataLevel> onSetLevel; 
        public event Action<int,string> onSetHint; 
        public event Action<AnswerResult> onSetResultAnswer; 
        public event Action<Sprite> onSetSprite; 
        public event Action<int,int> onGameFinal; 
       
// Back        
        public void SetDataLevel(ImportantDataLevel importantDataLevel)
        {
            onSetLevel?.Invoke(importantDataLevel);
        }

        public void SetNewHint(int countHint,string hintText)
        {
            onSetHint?.Invoke(countHint,hintText);
        }

        public void GameFinal(int valueCorrectAnswer, int valueQuestion)
        {
            onGameFinal?.Invoke(valueCorrectAnswer,valueQuestion);
        }

        public void SetResultAnswer(AnswerResult resultAnswer)
        {
            onSetResultAnswer?.Invoke(resultAnswer);
        }

        public void SetSpriteForOptionalAnswer(Sprite sprite)
        {
            onSetSprite?.Invoke(sprite);
        }
        
// UI
        public void SendMessageAboutExpireTime()
        {
            onExpireTime?.Invoke();
        }

        public void SendRequestNextHint()
        {
            onNextHint?.Invoke();
        }
        public void SendRequestForCheckAnswer(Sprite sprite)
        {
            onCheckAnswer?.Invoke(sprite);
        }
    }

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

    public interface IUIModel
    {
        public event Action<ImportantDataLevel> onSetLevel;
        public event Action<int,string> onSetHint; 
        public event Action<AnswerResult> onSetResultAnswer;
        public event Action<Sprite> onSetSprite;
        public event Action<int,int> onGameFinal; 
        
        public void SendRequestNextHint();
        public void SendRequestForCheckAnswer(Sprite sprite);
        public void SendMessageAboutExpireTime();
    }
}