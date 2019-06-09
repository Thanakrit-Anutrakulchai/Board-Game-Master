using UnityEngine;
using UnityEngine.UI;

// type alias, (# of players, # of rows, # of cols, piece resolution)
using DimensionsData = System.Tuple<byte, byte, byte, byte>;
using System;


// Items displayed on the MakeGame canvas 
internal sealed class MakeGame : Process<MakeGame>, IAssociatedStateEnter<DimensionsData>,
    IAssociatedStateEnter<BoardInfo>, IAssociatedStateEnter<PieceInfo>,
    IAssociatedStateLeave<GameCreationHandler>, IAssociatedStateLeave<GameInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Button makePieceButton;
    [SerializeField] internal readonly Button makeBoardButton;
    [SerializeField] internal readonly Button makeRuleButton;
    [SerializeField] internal readonly Button setWinCondButton;
    [SerializeField] internal readonly Button doneButton;
    [SerializeField] internal readonly InputField nameInput;





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeGame;
    }



    public void OnEnterState(IAssociatedStateLeave<BoardInfo> prevState, BoardInfo boardMade) 
    {
        // TODO
        if (!(prevState is MakeBoard)) // guard against wrong state
        {
            throw new Exception("Unexpected previous state - mismatch with arguments passed");
        }
    }


    public void OnEnterState(IAssociatedStateLeave<DimensionsData> prevState, DimensionsData data)
    {
        // TODO
        if (!(prevState is ChooseBoardDim)) // guard against wrong state
        {
            throw new Exception("Unexpected previous state - mismatch with arguments passed");
        }
    }


    // entered after end of piece creation
    public void OnEnterState(IAssociatedStateLeave<PieceInfo> previousState, PieceInfo pieceMade)
    {
        // TODO
        if (!(previousState is MakePiece)) // guard against wrong state
        {
            throw new Exception("Unexpected previous state - mismatch with arguments passed");
        }
    }


    public GameCreationHandler OnLeaveState(IAssociatedStateEnter<GameCreationHandler> nextState)
    {
        // TODO
        return null;
    }



    public GameInfo OnLeaveState(IAssociatedStateEnter<GameInfo> nextState)
    {
        // TODO
        throw new NotImplementedException();
    }
}
