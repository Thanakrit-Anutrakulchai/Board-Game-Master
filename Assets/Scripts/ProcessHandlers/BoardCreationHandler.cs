using UnityEngine;

// script which controls the behaviour of the board customization panel 
//  in the board creation process 
public class BoardCreationHandler
{
    /*** STATIC VARIABLES ***/
    // default colour of the piece button currently selected
    public static Color selectedPieceColour =
        new Color(36 / 255f, 185/255f, 46/255f, 1);



    /*** INSTANCE VARIABLES ***/
    // size of board transform's scale 
    internal float assignedBoardSquareSize = 1f;
    internal float BoardSquareSize { get { return assignedBoardSquareSize * 10; } }
    // information on board being created
    public BoardInfo boardInfo;

    // information on the piece currently selected to be placed 
    //  (or if 'no piece' selected)
    public byte pieceSelected;
}
