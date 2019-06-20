using UnityEngine;
using UnityEngine.UI;

// Items for piece creation process
internal sealed class MakePiece : Process<MakePiece>, IAssociatedState<GameCreationHandler, PieceInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button doneButton;
    [SerializeField] internal InputField nameInput;
    [SerializeField] internal Text complainText;





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakePiece;
    }



    public void OnEnterState(IAssociatedStateLeave<GameCreationHandler> previousState, 
                                GameCreationHandler gameHandler)
    {
        SetupUIs();

        // prepares for the creation of a new piece
        PieceCreationHandler pceHandler = PieceCreationHandler.GetHandler();
        pceHandler.StartNewPiece();
    }



    // Make Piece -> Make Game 
    // Finalize, creates, and returns information aboug piece created
    public PieceInfo OnLeaveState(IAssociatedStateEnter<PieceInfo> nextState)
    {
        PieceCreationHandler handler = PieceCreationHandler.GetHandler();

        // destroys board displayed
        handler.VirtualBoardUsed.DestroyBoard();


        // checks name is alphanumeric (with spaces)
        string pceName = nameInput.text;
        bool validInput = Utility.EnsureProperName(pceName);

        if (validInput)
        {
            // returns piece made
            return handler.FinalizePiece(pceName);
        } 
        else 
        {
            complainText.text = 
                "Name must contain only digits, letters, and spaces";

            TransitionHandler.GetHandler().AbortTransition();
            return null;
        }
    }



    private void SetupUIs() 
    {
        // clear name input field 
        nameInput.text = "";
        complainText.text = "";
    }
}
