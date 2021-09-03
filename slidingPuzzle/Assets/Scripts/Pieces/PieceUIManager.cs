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

        public Piece MyPiece { get; private set; }
        
        public static event Action EvtMove;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = Vector3.zero;
        }

        public void InitPiece(int verticalIndex, int horizontalIndex, float pieceSize, int boardMaxSize, int initialValue)
        {
            MyPiece = new Piece(verticalIndex, horizontalIndex, pieceSize, boardMaxSize, initialValue);
            _text.text = initialValue.ToString();
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            gameObject.name = $"({MyPiece.VerticalIndex}|{MyPiece.HorizontalIndex})";
            
            _anchorMinPosition.x = MyPiece.GetHorizontalMinPosition;
            _anchorMinPosition.y = MyPiece.GetVerticalMaxPosition;
            _anchorMaxPosition.x = MyPiece.GetHorizontalMaxPosition;
            _anchorMaxPosition.y = MyPiece.GetVerticalMinPosition;
            
            _rectTransform.anchorMin = _anchorMinPosition;
            _rectTransform.anchorMax = _anchorMaxPosition;
        }

        public void OnClick()
        {
            bool hasSameAxis = MyPiece.SameAxis(BoardManager.FreePiece.MyPiece);
        
            if(!hasSameAxis)
                return;
        
            SwitchPosition(BoardManager.FreePiece);
        }

        private void SwitchPosition(PieceUIManager otherPieceUIManager)
        {
            MyPiece.SwitchPosition(otherPieceUIManager.MyPiece);
            
            UpdatePosition();
            otherPieceUIManager.UpdatePosition();
            
            EvtMove?.Invoke();
        }

        public int GetIndex()
        {
            return MyPiece.GetIndex();
        }
    }
}