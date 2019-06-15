using UnityEngine;


public class WinCondCreationHandler : ProcessHandler<WinCondCreationHandler>
{
    /*** STATIC VARIABLES ***/
    internal static Color selectedPieceColour =
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);





    /*** INSTANCE VARIABLES ***/
    // info for win condition being made
    internal byte[,] winStructure;
    internal byte winner;

    internal byte pieceSelected;





    /*** INSTANCE PROPERTIES ***/
    internal VirtualBoard<PieceSpawningSlot> VirtualBoardUsed { get; set; }





    /*** INSTANCE METHODS ***/
    internal WinCondInfo FinalizeWinCond(string nm) 
    {
        // destroys board
        VirtualBoardUsed.DestroyBoard();

        // creates and returns win condition info
        WinCondInfo winCondMade = new WinCondInfo(name, winStructure, winner); 
        return winCondMade;
    }



    internal void StartNewWinCond(byte size, byte winningPlayer) 
    {
        // resets old stored info
        winStructure = new byte[size, size];
        winStructure.ReplaceAllWith((i, j) => PieceInfo.noPiece);
        winner = winningPlayer;

        pieceSelected = PieceInfo.noPiece;

        // generates new board 
        GameCreationHandler gameHandler = GameCreationHandler.GetHandler();

        VirtualBoardUsed = new VirtualBoard<PieceSpawningSlot>
            (
                winStructure, 
                gameHandler.pieceResolution,  
                gameHandler.SquareSize,
                gameHandler.GapSize, 
                (vboard, r, c) => 
                {
                    // toggle piece
                    if (pieceSelected == winStructure[r, c])
                    {
                        winStructure[r, c] = PieceInfo.noPiece;
                    }
                    else
                    {
                        winStructure[r, c] = pieceSelected;
                    }
                }
            );

        // spawns board
        VirtualBoardUsed.SpawnBoard(SpatialConfigs.commonBoardOrigin);
    }
}
