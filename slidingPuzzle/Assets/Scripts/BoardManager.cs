using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    //8 pedaçoes e 1 espaço
    //click no pedaço mover  pro lado livre
    
    //8 pedaçoes ja na cena
    //na inicializaçao -> posicionar
    
    //cada pedaço 
    //on click de cada pedaço vai chamar a um metodo no board pra saber aonde ele tem q ir
    //embaralhar e resetar
    
    /*
     * 00 01 02
     * 10 11 12
     * 20 21 22
     */

    [SerializeField] private List<PieceManager> _pieceManagers;

    private List<int> _result = new List<int> {1, 2, 3, 4, 5, 6, 7, -1, 8};

    public static int _freePieceVerticalIndex = 2;
    public static int _freePieceHorizontalIndex = 2;

    private void Awake()
    {
        PieceManager.EvtMove += CheckResult;
    }
    
    private void OnDestroy()
    {
        PieceManager.EvtMove -= CheckResult;
    }

    public void CheckResult()
    {
        for (var index = 0; index < _pieceManagers.Count; index++)
        {
            PieceManager piece = _pieceManagers[index];

            string pieceValueString = piece._text.text;

            int pieceValue = Convert.ToInt32(pieceValueString);

            if(_result[piece.GetIndex()] == pieceValue)
                continue;

            Debug.Log("Fail");
            return;
        }

        Debug.Log("success");
    }
}
