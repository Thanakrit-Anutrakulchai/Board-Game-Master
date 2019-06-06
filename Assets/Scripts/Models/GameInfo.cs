﻿using System.Collections.Generic;
using System;

/* CURRENT LIST OF CONSTRAINTS ON A GAME
 *  BOARD SIZE: 256 x 256 max, at least 1 by 1
 *  MAX NUMBER OF PIECES: 255
 * 
 * 
 * 
 */


// class representing the information needed to play a custom game 
[System.Serializable]
public class GameInfo
{
    /*** STATIC VARIABLES ***/
    // the max number of types of pieces is capped by the representing type
    //        As of the first version, this is byte, and capped at 
    //        254 pieces (possibility of no piece on a square, or no square at all)
    private const byte maxNumOfPieces = byte.MaxValue - 1;


    /*** INSTANCE VARIABLES ***/
    // number of players
    public byte NumberOfPlayers { get; set; }

    // state of the board at the start of the game
    public BoardInfo boardAtStart;

    // board sizes in number of squares (which can have full pieces on top)
    public byte numOfRows;
    public byte numOfCols;

    // pieces of the game, with indexes used as a sort of identifier
    public List<PieceInfo> pieces;

    // the "resolution in cubes" of the piece 
    //  would be 'n' for pieces made on an n x n grid
    public byte pieceResolution;

    // number of pieces declared so far;
    // NOTE: This should be used instead of pieces.length
    public byte numOfPieces;

    // the rules of the game which triggers when a piece is clicked
    public Dictionary<byte, List<RuleInfo>> rules;

    // absolute win conditions, game states where a player wins 
    //  the tagged byte represents the player who wins 
    public List<Tuple<Game, byte>> absoluteWinConditions;



    /*** INSTANCE PROPERTIES ***/
    // size of the piece spawning slots used for tiling this 
    public float spawnSlotSize
    {
        get
        {
            return boardAtStart.squareSize / pieceResolution;
        }
    }





    /*** CONSTRUCTORS ***/
    public GameInfo(BoardInfo brdStrt, List<PieceInfo> pcs)
    {
        this.boardAtStart = brdStrt;
        this.pieces = pcs;
        this.numOfRows = brdStrt.numOfRows;
        this.numOfCols = brdStrt.numOfCols;

        // starts off with no rules specified
        this.rules = new Dictionary<byte, List<RuleInfo>>();

        // starts off with no specified win conditions 
        this.absoluteWinConditions = new List<Tuple<Game, byte>>();
    }



    /*** STATIC METHODS ***/
    // randomly place pieces on the empty slots on the old board
    public BoardInfo RandomPiecePlacements(BoardInfo oldBoard) 
    {
        System.Random ranGen = new System.Random();

        for (byte r = 0; r < oldBoard.numOfRows; r++) 
        { 
            for (byte c = 0; c < oldBoard.numOfCols; c++) 
            {
                byte ranPiece =  (byte)ranGen.Next(numOfPieces + 1);
                if (ranPiece == numOfPieces)
                {
                    ranPiece = PosInfo.noPiece;
                }

                oldBoard.boardStateRepresentation[r, c] =
                    ranPiece;
            }
        }

        return oldBoard;
    }


    /*** INSTANCE METHODS ***/
    // trys to add piece to the list of pieces and returns true iff successful
    public bool AddPiece(PieceInfo pce) 
    { 
        if (numOfPieces < maxNumOfPieces) 
        {
            pieces.Add(pce);
            numOfPieces++;

            return true;
        }
        else // cannot add anymore if max number of pieces is reached
        {
            return false;
        }
    }

}
