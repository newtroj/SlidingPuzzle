using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Custom/ScriptableObjects/BoardConfig")]
    public class BoardConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private int _boardMaxSize;
        [SerializeField] private List<int> _result;

        public IReadOnlyList<int> Result => _result;
        
        public int GetBoardMaxSize => _boardMaxSize;
        
        public float GetPieceMaxSize => 1f / GetBoardMaxSize;
    }
}