using System;
using UIGame.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIGame.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private QuestionData[] _levelData;
        [SerializeField] private ViewObject _viewObject;

        private UIView UIView => _viewObject._uiView;
        private UIMover UIMover => _viewObject._uiMover;
        
        [Serializable]
        private struct ViewObject
        {
            public UIView _uiView;
            public UIMover _uiMover;
        }
        
        private void OnValidate()
        {
            if (_levelData == null)
                Debug.LogError("Level Data is NULL");
            
        }

        private void Start()
        {
            var gameModel = new GameModel();

            var uiController = new UIController(gameModel,UIView,UIMover);

            var levelHandler = new LevelHandler(_levelData,gameModel);
        }
    }
}