using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    /*
     * 00 01 02
     * 10 11 12
     * 20 21 22
     */

    [SerializeField] private BoardConfigScriptableObject _boardConfig;
    [SerializeField] private List<PieceUIManager> _pieceManagers;

    [SerializeField] private Transform _pieceUIManagerPrefab;
    [SerializeField] private Transform _pieceParentTransform;

    private List<int> _result = new List<int> {1, 2, 3, 4, 5, 6, 7, -1, 8};

    public static Piece FreePiece;

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
        int boardVerticalSize = _boardConfig.GetBoardVerticalSize;
        int boardHorizontalSize = _boardConfig.GetBoardHorizontalSize;

        float pieceVerticalSize = _boardConfig.GetPieceVerticalSize;
        float pieceHorizontalSize = _boardConfig.GetPieceHorizontalSize;

        FreePiece = new Piece(2, 2, _boardConfig.GetPieceVerticalSize, _boardConfig.GetPieceHorizontalSize, boardVerticalSize);
        
        for (int i = 0; i < boardHorizontalSize; i++)
        {
            for (int j = 0; j < boardVerticalSize; j++)
            {
                CreatePiece(j, i, pieceVerticalSize, pieceHorizontalSize, boardVerticalSize);
            }
        }
    }

    private void CreatePiece(int j, int i, float pieceVerticalSize, float pieceHorizontalSize, int boardVerticalMaxSize)
    {
        Transform pieceUIManagerTransform = Instantiate(_pieceUIManagerPrefab, Vector3.zero, Quaternion.identity, _pieceParentTransform);
        PieceUIManager pieceUIManager = pieceUIManagerTransform.GetComponent<PieceUIManager>();
        pieceUIManager.InitPiece(j, i, pieceVerticalSize, pieceHorizontalSize, boardVerticalMaxSize);
        
        _pieceManagers.Add(pieceUIManager);
    }

    public void CheckResult()
    {
        for (var index = 0; index < _pieceManagers.Count; index++)
        {
            PieceUIManager pieceUIManager = _pieceManagers[index];

            string pieceValueString = pieceUIManager._text.text;

            int pieceValue = Convert.ToInt32(pieceValueString);

            if(_result[pieceUIManager.GetIndex()] == pieceValue)
                continue;

            Debug.Log("Fail");
            return;
        }

        Debug.Log("success");
    }
}