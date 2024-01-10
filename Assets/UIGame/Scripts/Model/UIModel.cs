using System;
using UIGame.Scripts.Back;
using UIGame.Scripts.Settings;
using UnityEngine;

namespace UIGame.Scripts.Model
{
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