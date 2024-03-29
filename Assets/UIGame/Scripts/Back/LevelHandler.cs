using UIGame.Scripts.Settings;
using UnityEngine;

namespace UIGame.Scripts.Back
{
    public class LevelHandler
    {
        public LevelHandler(QuestionData[] questionData,IBackModel backModel )
        {
            _questionData = questionData;
            _backModel = backModel;

            _backModel.onNextHint += SetNextHint;
            _backModel.onCheckAnswer += CheckAnswer;
            _backModel.onExpireTime += ExpireTime;
            
            SetLevel();
        }

        private readonly IBackModel _backModel;
        private readonly QuestionData[] _questionData;
        
        private  int _countLevelNow = -1;
        private  int _countHintNow;
        private int _valueCorrectAnswer;

        private void NewLevel()
        {
            var newLevel = _countLevelNow + 1 ;
            
            if (newLevel >= _questionData.Length)
            {
               _backModel.GameFinal(_valueCorrectAnswer,_questionData.Length);
               
               return;
            }
            
            _countHintNow = 0;
            
            SetLevel();
        }
        
        private void SetLevel()
        {
            _countLevelNow++;
            
            var data = new ImportantDataLevel
            {
                levelCount = _countLevelNow + 1,
                hintCount =  _countHintNow + 1,
                
                hintText = _questionData[_countLevelNow].Hint[_countHintNow],
                timer =  _questionData[_countLevelNow].TimeToRespond,
            };
            
            
            _backModel.SetDataLevel(data);
            
            _countHintNow ++;
            
            SetOptionalAnswer();
        }

        private void SetOptionalAnswer()
        {
            var sprites = _questionData[_countLevelNow].OptionalAnswer;

            foreach (var sprite in sprites)
            {
                _backModel.SetSpriteForOptionalAnswer(sprite);
            }
        }

        private void SetNextHint()
        {
            while (true)
            {
                var hintCount = _countHintNow + 1;

                if (_countHintNow >= _questionData[_countLevelNow].Hint.Length)
                {
                    _countHintNow = 0;

                    continue;
                }

                _backModel.SetNewHint(hintCount, _questionData[_countLevelNow].Hint[_countHintNow]);

                _countHintNow++;
                break;
            }
        }

        private void CheckAnswer(Sprite sprite)
        {
            if (sprite == _questionData[_countLevelNow].CorrectAnswer)
            {
                _valueCorrectAnswer++;
                _backModel.SetResultAnswer(AnswerResult.Correct);
                NewLevel();
            }
            else
            {
                _backModel.SetResultAnswer(AnswerResult.UnCorrect);
            }
        }

        private void ExpireTime()
        {
            _backModel.SetResultAnswer(AnswerResult.Expire);
            
            NewLevel();
        }
    }

    public struct ImportantDataLevel
    {
        public int levelCount;
        public int hintCount;
        public string hintText;
        public int timer;
    }
}
