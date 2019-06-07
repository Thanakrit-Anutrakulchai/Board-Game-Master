﻿using UnityEngine;

// script which controls the behaviour of the board creation process 
public class BoardCreationHandler
{
    /*** STATIC VARIABLES ***/
    // default colour of the piece button currently selected
    public static Color selectedPieceColour =
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);

    // default PosInfo colour of a new board
    public static PosInfo.RGBData defaultBoardColour =
        new PosInfo.RGBData(22, 22, 22);



    /*** INSTANCE VARIABLES ***/
    // size of board transform's scale 
    [SerializeField] internal float assignedBoardSquareSize = 1f;
    internal float BoardSquareSize { get { return assignedBoardSquareSize * 10; } }

    // information on board being created
    private BoardInfo boardBeingMade;

    // information on the piece currently selected to be placed 
    //  (or if 'no piece' selected)
    private byte pieceSelected;





    /*** INSTANCE PROPERTIES ***/
    internal byte PieceSelected { set => pieceSelected = value; }
    /// <summary>
    /// The VirtualBoard used for creating this piece, to delete
    /// </summary>
    internal VirtualBoard<PieceSpawningSlot> VirtualBoardUsed { get; set; }





    /*** STATIC METHODS ***/
    internal static BoardCreationHandler GetHandler()
    {
        return Camera.main.GetComponent<BoardCreationHandler>();
    }





    /*** INSTANCE METHODS ***/
    // ends board creation process, and returns completed board info
    internal BoardInfo FinishBoard() 
    {
        // destroys all piece slots used
        VirtualBoardUsed.DestroyBoard();
        // returns finalized board
        return boardBeingMade;
    }



    // clears all stored information and prepares to make new board
    //   returns a reference to the board being made
    internal BoardInfo StartNewBoard(byte rows, byte cols, float gapSize) 
    {
        pieceSelected = PieceInfo.noPiece;
        boardBeingMade = BoardInfo.DefaultBoard(rows, cols, BoardSquareSize,  gapSize, defaultBoardColour);
        return boardBeingMade;
    }


    /// <summary>
    /// Tries to place/remove piece selected on board being made at that position, 
    /// returns true iff successful
    /// </summary>
    internal bool SetPiece(byte row, byte col) 
    {
        try
        {
            boardBeingMade.BoardStateRepresentation[row, col] = pieceSelected;
            return true;
        }
        catch 
        {
            return false;
        }
    }
    
}
