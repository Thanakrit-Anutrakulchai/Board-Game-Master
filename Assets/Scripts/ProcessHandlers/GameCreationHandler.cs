using System;
using System.Collections.Generic;
using UnityEngine;


// script which controls the behaviour of the game creation process 
public class GameCreationHandler : ProcessHandler<GameCreationHandler>
{
    /*** INSTANCE VARIABLES ***/
    // board to be used in the game
    internal BoardInfo boardAtStart;

    // pieces to be used in the game, each piece is tied to its index in this list
    internal List<PieceInfo> pieces;

    // resolution of the pieces
    internal byte pieceResolution;

    // rules of the game, sorted according to player's turn and trigger piece 
    //   with noSquare denoting panel rules: rules[(player, trigPiece)] = info
    internal Dictionary<byte, Dictionary<byte, List<RuleInfo>>> rules;





    /*** INSTANCE PROPERTIES ***/
    internal byte NumOfRows 
    {
        get 
        {
            return boardAtStart.NumOfRows;
        }
    }

    internal byte NumOfCols
    {
        get
        {
            return boardAtStart.NumOfCols;
        }
    }

    internal float SizeOfGap 
    { 
        get 
        {
            return boardAtStart.SizeOfGap;
        }
    }






}
