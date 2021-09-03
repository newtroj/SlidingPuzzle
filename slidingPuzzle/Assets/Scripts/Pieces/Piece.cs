using System;

namespace DefaultNamespace
{
    public class Piece
    {
        private readonly float _verticalSize;
        private readonly float _horizontalSize;
        private readonly int _boardMaxSize;
        private readonly int _value;

        public int VerticalIndex { get; private set; }
        public int HorizontalIndex { get; private set; }
        
        public float GetHorizontalMinPosition => _horizontalSize * HorizontalIndex;
        public float GetHorizontalMaxPosition => GetHorizontalMinPosition + _horizontalSize;
            
        public float GetVerticalMinPosition => 1 - _verticalSize * VerticalIndex;
        public float GetVerticalMaxPosition => GetVerticalMinPosition - _verticalSize;
        
        public Piece(int verticalIndex, int horizontalIndex, float pieceSize, int boardMaxSize, int value)
        {
            SetIndex(verticalIndex, horizontalIndex);
            _verticalSize = pieceSize;
            _horizontalSize = pieceSize;
            _boardMaxSize = boardMaxSize;
            _value = value;
        }

        private void SetIndex(int verticalIndex, int horizontalIndex)
        {
            VerticalIndex = verticalIndex;
            HorizontalIndex = horizontalIndex;
        }

        public bool SameAxis(Piece other)
        {
            bool matchVerticalAxis = VerticalIndex == other.VerticalIndex;
            bool matchHorizontalAxis = HorizontalIndex == other.HorizontalIndex;

            int verticalDistance = Math.Abs(VerticalIndex - other.VerticalIndex);
            int horizontalDistance = Math.Abs(HorizontalIndex - other.HorizontalIndex);
            
            return (matchVerticalAxis && horizontalDistance == 1 ||
                   matchHorizontalAxis && verticalDistance == 1) &&
                   (_value == -1 || other._value == -1);
        }

        public void SwitchPosition(Piece otherPiece)
        {
            int tempHorizontalIndex = HorizontalIndex;
            int tempVerticalIndex = VerticalIndex;

            SetIndex(otherPiece.VerticalIndex, otherPiece.HorizontalIndex);
            otherPiece.SetIndex(tempVerticalIndex, tempHorizontalIndex);
        }
        
        public int GetIndex()
        {
            return (VerticalIndex * _boardMaxSize) + HorizontalIndex;
        }
    }
}