using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private BoardConfigScriptableObject _boardConfig;
    [SerializeField] private List<PieceUIManager> _pieceManagers;

    [SerializeField] private Transform _pieceUIManagerPrefab;
    [SerializeField] private Transform _pieceParentTransform;

    public static PieceUIManager FreePiece;

    private void Awake()
    {
        InitBoard();

        PieceUIManager.EvtMove += CheckResult;
    }
    
    private void OnDestroy()
    {
        PieceUIManager.EvtMove -= CheckResult;
    }
    
    private void InitBoard()
    {
        int boardMaxSize = _boardConfig.GetBoardMaxSize;
        float pieceMaxSize = _boardConfig.GetPieceMaxSize;

        
        for (int i = 0; i < _boardConfig.Result.Count; i++)
        {
            int verticalIndex = i / boardMaxSize;
            int horizontalIndex = i % boardMaxSize;
            
            CreatePiece(verticalIndex, horizontalIndex, pieceMaxSize, boardMaxSize, _boardConfig.Result[i]);
        }
    }

    private void CreatePiece(int j, int i, float pieceSize, int boardMaxSize, int initialValue)
    {
        Transform pieceUIManagerTransform = Instantiate(_pieceUIManagerPrefab, Vector3.zero, Quaternion.identity, _pieceParentTransform);
        PieceUIManager pieceUIManager = pieceUIManagerTransform.GetComponent<PieceUIManager>();
        pieceUIManager.InitPiece(j, i, pieceSize, boardMaxSize, initialValue);
        
        _pieceManagers.Add(pieceUIManager);

        if (initialValue == -1)
        {
            FreePiece = pieceUIManager;
        }
    }

    public void CheckResult()
    {
        for (var index = 0; index < _pieceManagers.Count; index++)
        {
            PieceUIManager pieceUIManager = _pieceManagers[index];

            string pieceValueString = pieceUIManager._text.text;

            int pieceValue = Convert.ToInt32(pieceValueString);

            if(_boardConfig.Result[pieceUIManager.GetIndex()] == pieceValue)
                continue;

            Debug.Log("Fail");
            return;
        }

        Debug.Log("success");
    }
}