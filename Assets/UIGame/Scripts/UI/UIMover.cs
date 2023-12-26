using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIGame.Scripts.UI
{
    public class UIMover : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public event Action<Vector3,Sprite> onChangePosition;
        
        [SerializeField] private Canvas _canvas;

        private RectTransform _rectTransform;
        private Sprite _sprite;
        private Vector3 _beginPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            var selectedObject = eventData.pointerPressRaycast.gameObject;

            if (!selectedObject.CompareTag($"Answer")) return;

            _rectTransform = selectedObject.GetComponent<RectTransform>();
            _sprite = selectedObject.GetComponent<Image>().sprite;
            _beginPosition = _rectTransform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_rectTransform == null) return;

            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(_rectTransform == null) return;

            var worldPos = _rectTransform.position;
            
            onChangePosition?.Invoke(worldPos,_sprite);
            
            _rectTransform.position = _beginPosition;

            _rectTransform = null;
        }
    }
}