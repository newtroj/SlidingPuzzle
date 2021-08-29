using System;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

namespace DefaultNamespace
{
    public class PieceUIManager : MonoBehaviour
    {
        public Text _text;
        
        private RectTransform _rectTransform;
        private Vector2 _anchorMinPosition = Vector2.zero;
        private Vector2 _anchorMaxPosition = Vector2.zero;
        
        private Piece _myPiece;
        
        public static event Action EvtMove;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = Vector3.zero;
        }

        public void InitPiece(int verticalIndex, int horizontalIndex, float verticalSize, float horizontalSize, int boardVerticalMaxSize)
        {
            _myPiece = new Piece(verticalIndex, horizontalIndex, verticalSize, horizontalSize, boardVerticalMaxSize);
            _text.text = _myPiece.GetIndex().ToString();
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            _anchorMinPosition.x = _myPiece.GetHorizontalMinPosition;
            _anchorMinPosition.y = _myPiece.GetVerticalMaxPosition;
            _anchorMaxPosition.x = _myPiece.GetHorizontalMaxPosition;
            _anchorMaxPosition.y = _myPiece.GetVerticalMinPosition;
            
            _rectTransform.anchorMin = _anchorMinPosition;
            _rectTransform.anchorMax = _anchorMaxPosition;
        }

        public void OnClick()
        {
            bool hasSameAxis = _myPiece.SameAxis(BoardManager.FreePiece);
        
            if(!hasSameAxis)
                return;
        
            SwitchPosition(BoardManager.FreePiece);
        }

        private void SwitchPosition(Piece otherPiece)
        {
            _myPiece.SwitchPosition(otherPiece);
            
            UpdatePosition();
            EvtMove?.Invoke();
        }

        public int GetIndex()
        {
            return _myPiece.GetIndex();
        }
    }
}