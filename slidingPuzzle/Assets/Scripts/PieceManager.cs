using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PieceManager : MonoBehaviour
    {
        [SerializeField] private int _verticalIndex;
        [SerializeField] private int _horizontalIndex;
        [SerializeField] private float _verticalSize;
        [SerializeField] private float _horizontalSize;

        public Text _text;
        
        private RectTransform _rectTransform;

        public static event Action EvtMove;  
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            UpdatePosition();
        }
        
        private void UpdatePosition()
        {
            float horizontalMinPosition = _horizontalSize * _horizontalIndex;
            float horizontalMaxPosition = horizontalMinPosition + _horizontalSize;
            
            float verticalMinPosition = 1 - (_verticalSize * _verticalIndex);
            float verticalMaxPosition = verticalMinPosition - _verticalSize;
            
            _rectTransform.anchorMin = new Vector2(horizontalMinPosition, verticalMaxPosition);
            _rectTransform.anchorMax = new Vector2(horizontalMaxPosition, verticalMinPosition);
        }

        public void OnClick()
        {
            bool hasSameAxis = SameAxis(BoardManager._freePieceVerticalIndex, BoardManager._freePieceHorizontalIndex);
        
            if(!hasSameAxis)
                return;
        
            SwitchPosition(ref BoardManager._freePieceVerticalIndex, ref BoardManager._freePieceHorizontalIndex);
        }

        public bool SameAxis(int verticalIndex, int horizontalIndex)
        {
            bool matchVerticalAxis = _verticalIndex == verticalIndex;
            bool matchHorizontalAxis = _horizontalIndex == horizontalIndex;

            return (matchVerticalAxis && Mathf.Abs(_horizontalIndex - horizontalIndex) == 1) ||
                   (matchHorizontalAxis && Mathf.Abs(_verticalIndex - verticalIndex) == 1);
        }

        public void SwitchPosition(ref int verticalIndex, ref int horizontalIndex)
        {
            int tempHorizontalIndex = _horizontalIndex;
            int tempVerticalIndex = _verticalIndex;

            _horizontalIndex = horizontalIndex;
            _verticalIndex = verticalIndex;

            verticalIndex = tempVerticalIndex;
            horizontalIndex = tempHorizontalIndex;
            
            UpdatePosition();
            
            EvtMove?.Invoke();
        }

        public int GetIndex()
        {
            return (_verticalIndex * 3) + _horizontalIndex;
        }
    }
}