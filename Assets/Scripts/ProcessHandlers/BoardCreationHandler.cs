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
    [SerializeField] internal float assignedBoardSquareSize = 1f;
    internal float BoardSquareSize { get { return assignedBoardSquareSize * 10; } }
    
    // information on board being created
    private BoardInfo boardBeingMade;

    // information on the piece currently selected to be placed 
    //  (or if 'no piece' selected)
    private byte pieceSelected;





    /*** INSTANCE PROPERTIES ***/
    internal byte PieceSelected { set => pieceSelected = value; }





    /*** STATIC METHODS ***/
    internal static BoardCreationHandler GetHandler()
    {
        return Camera.main.GetComponent<BoardCreationHandler>();
    }





    /*** INSTANCE METHODS ***/
    // clears all stored information and prepares to make new board
    internal void StartNewBoard() 
    {
        pieceSelected = PieceInfo.noPiece;

    }
}
