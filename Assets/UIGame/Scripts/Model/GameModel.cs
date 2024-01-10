using System;
using UIGame.Scripts.Back;
using UIGame.Scripts.Settings;
using UnityEngine;

namespace UIGame.Scripts.Model
{
    // Back Partial
    public partial class GameModel : IBackModel, IUIModel
    {
        public event Action onNextHint; 
        public event Action<Sprite> onCheckAnswer; 
        public event Action onExpireTime; 
        
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
    }

    
    // UI partial
    public partial class GameModel
    {
        public event Action<ImportantDataLevel> onSetLevel; 
        public event Action<int,string> onSetHint; 
        public event Action<AnswerResult> onSetResultAnswer; 
        public event Action<Sprite> onSetSprite; 
        public event Action<int,int> onGameFinal;
        
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
}