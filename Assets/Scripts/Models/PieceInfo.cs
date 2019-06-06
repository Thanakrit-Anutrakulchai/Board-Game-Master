using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class containing information about a piece, e.g. graphics 
[System.Serializable]
public class PieceInfo
{
    /*** STATIC VARIABLES ***/
    // the byte value used to represent the lack of a piece on a square
    //  used in array representation of where pieces are on a board 
    internal const byte noPiece = byte.MaxValue - 1;

    // byte to represent lack of a square 
    internal const byte noSquare = byte.MaxValue;
    

    /*** INSTANCE VARIABLES ***/
    // 2D array representing the piece
    public PosInfo[,] visualRepresentation; 
    // name of piece
    public string pieceName;
    
    public PieceInfo(string name, PosInfo[,] visRep)  
    {
        this.pieceName = name;
        this.visualRepresentation = visRep;
    }


}
