using UnityEngine;

namespace UIGame.Scripts
{
    [CreateAssetMenu(fileName = "New Question Data", menuName = "Question Data", order = 53)]
    public class QuestionData : ScriptableObject
    {
        [SerializeField] private int _timeToRespond;
        [SerializeField] private Sprite _correctAnswer;
        [SerializeField] private Sprite[] _optionalAnswer;
        [SerializeField] private string[] _hint;
        
        public int TimeToRespond => _timeToRespond;
        public string[] Hint => _hint;
        public Sprite[] OptionalAnswer=> _optionalAnswer;

        public Sprite CorrectAnswer => _correctAnswer;
        
        private void OnValidate()
        {
            if (_timeToRespond <= 0)
                _timeToRespond = 1;

            if (_hint.Length == 0)
                Debug.LogError("Hint is NULL!");
            
            if (_optionalAnswer.Length == 0)
                Debug.LogError("Optional Answer is NULL!");
            
            if (_correctAnswer == null)
                Debug.LogError("Correct Question is NULL!");
        }
    }
}