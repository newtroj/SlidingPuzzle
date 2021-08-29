using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Custom/ScriptableObjects/BoardConfig")]
    public class BoardConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private int _boardVerticalSize;
        [SerializeField] private int _boardHorizontalSize;

        public int GetBoardVerticalSize => _boardVerticalSize;
        public int GetBoardHorizontalSize => _boardHorizontalSize;
        
        public float GetPieceVerticalSize => 1f / _boardVerticalSize;
        public float GetPieceHorizontalSize => 1f / _boardHorizontalSize;
    }
}