using System;
using TMPro;
using UIGame.Scripts.Back;
using UIGame.Scripts.Settings;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace UIGame.Scripts.UI
{
    public partial class UIView : MonoBehaviour
    {
        public event Action onNewHint;

        [SerializeField] private RectTransform _fieldChecksAnswer;
        [SerializeField] private Button _nextHintButton;
        [SerializeField] private PointAnswer[] _pointAnswer;

        [SerializeField] private QuestionInfo _questionInfo;
        [SerializeField] private FieldQuestionObject _fieldQuestionObject;
        [SerializeField] private ResultAnswer _resultAnswer;
        [SerializeField] private EndTable _endTable;

        private int _requests;

        private void Start()
        {
            _endTable._parentCanvas.enabled = false;
            
            _nextHintButton.onClick.AddListener(NextHint);
        }

        private void NextHint()
        {
            onNewHint?.Invoke();
        }

        public void NextLevel(ImportantDataLevel importantDataLevel)
        {
            var levelCount = importantDataLevel.levelCount;
            var hintText = importantDataLevel.hintText;
            var hintCount = importantDataLevel.hintCount;


            ChangeCountLevel(levelCount);

            ChangeHint(hintText, hintCount);
        }

        private void ChangeCountLevel(int level)
        {
            if (level < 0)
                level = 0;

            _questionInfo._number.text = level.ToString();
        }

        public void ChangeHint(string hint, int countHint)
        {
            if (string.IsNullOrEmpty(hint) || hint == " ")
                _fieldQuestionObject._textHint.text = "Сегодня без подсказок";

            if (countHint <= 0)
                countHint = 1;

            _fieldQuestionObject._numberHint.text = countHint.ToString();
            _fieldQuestionObject._textHint.text = hint;
        }

        public void ChangeResult(AnswerResult result)
        {
            switch (result)
            {
                case AnswerResult.Correct:
                    _questionInfo._result.text = _resultAnswer._correctAnswer;
                    _questionInfo._result.color = _resultAnswer._correctColor;

                    ChangeSpriteToDefault();
                    break;
                case AnswerResult.UnCorrect:
                    _questionInfo._result.text = _resultAnswer._unCorrectAnswer;
                    _questionInfo._result.color = _resultAnswer._unCorrectColor;
                    break;
                case AnswerResult.Expire:
                    _questionInfo._result.text = _resultAnswer._expireAnswer;
                    _questionInfo._result.color = _resultAnswer._unCorrectColor;
                    break;
            }
        }

        private void ChangeSpriteToDefault()
        {
            _requests = 0;
            foreach (var sprite in _pointAnswer)
            {
                var image = sprite.GetComponent<Image>();

                image.sprite = null;
            }
        }

        public void SetSpriteForAnswer(Sprite sprite)
        {
            if (_requests >= _pointAnswer.Length)
                return;
            _requests++;

            var ran = new Random();

            __RandomPoint();
            return;

            void __RandomPoint()
            {
                while (true)
                {
                    var spritePoint = ran.Next(0, _pointAnswer.Length);

                    var go = _pointAnswer[spritePoint].gameObject;

                    var image = go.GetComponent<Image>();

                    if (image.sprite != null)
                    {
                        continue;
                    }

                    image.sprite = sprite;
                    break;
                }
            }
        }

        public Vector3[] GetSidesField()
        {
            var vectorAllPos = new Vector3[4];

            _fieldChecksAnswer.GetWorldCorners(vectorAllPos);

            var leftUpPos = vectorAllPos[0];
            var rightDownPos = vectorAllPos[2];


            return new[] { leftUpPos, rightDownPos };
        }

        public void ChangeTimer(int time)
        {
            if (time < 0)
                time = 0;

            _fieldQuestionObject._timer.text = $"{time} sec";
        }
        
        public void ChangeTimerExit(int time)
        {
            if (time < 0)
                time = 0;

            _endTable._timerText.text = time.ToString();
        }

        public void SetEndGameTable(int valueCorrectAnswer,int valueQuestion)
        {
            _endTable._valueCorrectAnswerText.text = valueCorrectAnswer.ToString();
            _endTable._valueQuestionText.text = valueQuestion.ToString();

            if (valueCorrectAnswer == 0)
                valueCorrectAnswer = 1;
            
            var result = valueQuestion / valueCorrectAnswer;

            _endTable._resultText.text = valueCorrectAnswer > result ? _endTable._goodResult : _endTable._badResult;
            
            _endTable._parentCanvas.enabled = true;
        }
    }

    // Decorate private struct
    public partial class UIView
    {
        [Serializable]
        private struct EndTable
        {
            public Canvas _parentCanvas;
            public TMP_Text _valueCorrectAnswerText;
            public TMP_Text _valueQuestionText;
            public TMP_Text _resultText;
            public TMP_Text _timerText;

            public string _goodResult;
            public string _badResult;
        }

        [Serializable]
        private struct ResultAnswer
        {
            public string _correctAnswer;
            public string _unCorrectAnswer;
            public string _expireAnswer;

            public Color _unCorrectColor;
            public Color _correctColor;
        }

        [Serializable]
        private struct QuestionInfo
        {
            public TMP_Text _number;
            public TMP_Text _result;
        }

        [Serializable]
        private struct FieldQuestionObject
        {
            public TMP_Text _timer;
            public TMP_Text _numberHint;
            public TMP_Text _textHint;
        }
    }
}