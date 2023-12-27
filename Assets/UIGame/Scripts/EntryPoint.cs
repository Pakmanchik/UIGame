using System;
using UIGame.Scripts.Back;
using UIGame.Scripts.Settings;
using UIGame.Scripts.UI;
using UnityEngine;

namespace UIGame.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private QuestionData[] _levelData;
        [SerializeField] private ViewObjects _viewObjects;

        private UIView UIView => _viewObjects._uiView;
        private UIMover UIMover => _viewObjects._uiMover;
        
        [Serializable]
        private struct ViewObjects
        {
            public UIView _uiView;
            public UIMover _uiMover;
        }
        
        private void OnValidate()
        {
            if (UIView == null)
                Debug.LogError("UIView is NULL");
            if (UIMover == null)
                Debug.LogError("UIMover is NULL");
            
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