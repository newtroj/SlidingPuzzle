using System;

namespace DefaultNamespace
{
    public class Piece
    {
        private int _verticalIndex;
        private int _horizontalIndex;
        private readonly float _verticalSize;
        private readonly float _horizontalSize;
        private readonly int _boardVerticalMaxSize;
        
        public float GetHorizontalMinPosition => _horizontalSize * _horizontalIndex;
        public float GetHorizontalMaxPosition => GetHorizontalMinPosition + _horizontalSize;
            
        public float GetVerticalMinPosition => 1 - _verticalSize * _verticalIndex;
        public float GetVerticalMaxPosition => GetVerticalMinPosition - _verticalSize;
        
        public Piece(int verticalIndex, int horizontalIndex, float verticalSize, float horizontalSize, int boardVerticalMaxSize)
        {
            SetIndex(verticalIndex, horizontalIndex);
            _verticalSize = verticalSize;
            _horizontalSize = horizontalSize;
            _boardVerticalMaxSize = boardVerticalMaxSize;
        }

        private void SetIndex(int verticalIndex, int horizontalIndex)
        {
            _verticalIndex = verticalIndex;
            _horizontalIndex = horizontalIndex;
        }

        public bool SameAxis(Piece other)
        {
            bool matchVerticalAxis = _verticalIndex == other._verticalIndex;
            bool matchHorizontalAxis = _horizontalIndex == other._horizontalIndex;

            return matchVerticalAxis && Math.Abs(_horizontalIndex - other._horizontalIndex) == 1 ||
                   matchHorizontalAxis && Math.Abs(_verticalIndex - other._verticalIndex) == 1;
        }

        public void SwitchPosition(Piece otherPiece)
        {
            int tempHorizontalIndex = _horizontalIndex;
            int tempVerticalIndex = _verticalIndex;

            SetIndex(otherPiece._horizontalIndex, otherPiece._verticalIndex);
            otherPiece.SetIndex(tempVerticalIndex, tempHorizontalIndex);
        }
        
        public int GetIndex()
        {
            return (_verticalIndex * _boardVerticalMaxSize) + _horizontalIndex;
        }
    }
}