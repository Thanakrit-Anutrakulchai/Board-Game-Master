using UnityEngine;
using UnityEngine.UI;

// Items for piece creation process
internal sealed class MakePiece : Process<MakePiece>, IAssociatedState<GameCreationHandler, PieceInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Button doneButton;
    [SerializeField] internal readonly InputField nameInput;





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakePiece;
    }



    public void OnEnterState(IAssociatedStateLeave<GameCreationHandler> previousState, 
                                GameCreationHandler gameHandler)
    {
        // prepares for the creation of a new piece
        PieceCreationHandler pceHandler = PieceCreationHandler.GetHandler();
        BoardInfo creationSquare = pceHandler.StartNewPiece(gameHandler.pieceResolution);

        VirtualBoard<PieceBuildingSlot> vBoard = new VirtualBoard<PieceBuildingSlot>
            (
                creationSquare,
                gameHandler.pieces,
                gameHandler.pieceResolution,
                Prefabs.GetPrefabs().pieceBuildingSlot,
                (brd, r, c) => 
                {
                    PosInfo curColour = pceHandler.pieceBeingMadeRep[r, c];
                    switch (curColour) 
                    {
                        case PosInfo.RGBData colour:
                            pceHandler.pieceBeingMadeRep[r, c] = new PosInfo.Nothing();
                            break;
                        case PosInfo.Nothing none:
                            // TODO TEMP add ability to choose colours!
                            pceHandler.pieceBeingMadeRep[r, c] =
                                new PosInfo.RGBData(0, 0, 0); 
                            break;
                    }
                }
            );

        vBoard.SpawnBoard(SpatialConfigs.commonBoardSpawn);
    }



    // Make Piece -> Make Game 
    // Finalize, creates, and returns information aboug piece created
    public PieceInfo OnLeaveState(IAssociatedStateEnter<PieceInfo> nextState)
    {
        PieceCreationHandler handler = PieceCreationHandler.GetHandler();

        // TODO add checks (alphanum with spaces, not too long)
        // gets user inputted name of piece 
        string pceName = nameInput.text;

        // returns piece made
        return handler.FinalizePiece(pceName);
    }
}
