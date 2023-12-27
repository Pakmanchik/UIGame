using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIGame.Scripts.UI
{
    public class UIController
    {
        public UIController(IUIModel uiModel,UIView uiView,UIMover uiMover)
        {
            _uiModel = uiModel;
            _uiView = uiView;
            _uiMover = uiMover;

            _timeController = new TimeController();
            
            SubscribeOnAnyEvent();
            SubscribeOnEventUIModel();
            SubscribeOnEventUIMover();
        }
        
        private readonly IUIModel _uiModel;
        private readonly UIView _uiView;
        private readonly UIMover _uiMover;
        
        private readonly TimeController _timeController;

        private Sprite _sprite;
        private bool _endGame;

        private void SubscribeOnEventUIMover()
        {
            _uiMover.onChangePosition += (vector3, sprite) =>
            {
                _sprite = sprite;
               var arrVector = _uiView.GetSidesField();
               
               var leftUpPos = arrVector[0];
               var rightDownPos = arrVector[1];

               if (vector3.x > leftUpPos.x && vector3.y < rightDownPos.y && vector3.x < rightDownPos.x &&
                   vector3.y > leftUpPos.y)
               {
                   _uiModel.SendRequestForCheckAnswer(_sprite);
               }
            };
        }
        
        private void SubscribeOnAnyEvent()
        {
            _timeController.onTimeNow += time =>
            {
                if (_endGame)
                {
                    _uiView.ChangeTimerExit(time);
                    return;
                }
                _uiView.ChangeTimer(time);
            };
            
            _timeController.onTimeExpire += () =>
            {
                if (_endGame)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }
                _uiModel.SendMessageAboutExpireTime();
            };
            
            _uiView.onNewHint += () =>
            {
                _uiModel.SendRequestNextHint();
            };
        }

        private void SubscribeOnEventUIModel()
        {
            _uiModel.onSetResultAnswer += aR =>
            {
                _uiView.ChangeResult(aR);
                if(aR is AnswerResult.Correct or AnswerResult.Expire)
                    _timeController.StopTimer();
            };
            
            _uiModel.onSetSprite += s =>
            {
                _uiView.SetSpriteForAnswer(s);
            };
            
            _uiModel.onSetHint += (countHint, hintText) =>
            {
                _uiView.ChangeHint(hintText, countHint);
            };
            
            _uiModel.onSetLevel += levelData =>
            {
                _uiView.NextLevel(levelData);
                
                _timeController.StartTimer(levelData.timer);
            };

            _uiModel.onGameFinal += (valueCorrectAnswer, valueQuestion) =>
            {
                _endGame = true;
                _uiView.SetEndGameTable(valueCorrectAnswer, valueQuestion);
                _timeController.StopTimer();
                _timeController.StartTimer(5);
            };
        }
    }
}