using System;
using UnityEngine;
using UnityEngine.UI;

// type alias, (size of winning structure in squares) : Tuple<byte>
using WinCondSetupData = System.Tuple<byte, byte>;


// class for setting up information about win conditions
internal sealed class ChooseWinCondArea : Process<ChooseWinCondArea>,
    IAssociatedState<GameCreationHandler, WinCondSetupData>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button startButton;
    [SerializeField] internal InputField sizeInput;
    [SerializeField] internal InputField winnerInput;
    [SerializeField] internal Text complainText;





    /*** INSTANCE METHODS ***/
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseWinCondArea;
    }
    


    public Canvas GetCanvas()
    {
        return canvas;
    }



    public void OnEnterState(IAssociatedStateLeave<GameCreationHandler> previousState, GameCreationHandler gh)
    {
        SetupUIs();
    }



    public WinCondSetupData OnLeaveState(IAssociatedStateEnter<WinCondSetupData> nextState)
    {
        GameCreationHandler gameHandler = GameCreationHandler.GetHandler();

        // TODO allow rectangular (non-square) areas
        byte maxAreaSize = Math.Min(gameHandler.NumOfRows, gameHandler.NumOfCols);

        bool success = Byte.TryParse(sizeInput.text, out byte size) &&
            size.InRange(1, maxAreaSize);
        success &= Byte.TryParse(winnerInput.text, out byte winner) &&
            winner.InRange(1, gameHandler.numOfPlayers);

        if (success) 
        {
            return Tuple.Create(size, (byte) (winner - 1));
        }
        else
        {
            complainText.text = 
                "Please enter valid whole numbers";

            TransitionHandler.GetHandler().AbortTransition();
            return null;
        }
    }



    private void SetupUIs() 
    {
        complainText.text = "";

        // clears old input
        sizeInput.text = "";
        winnerInput.text = "";
    } 
}
