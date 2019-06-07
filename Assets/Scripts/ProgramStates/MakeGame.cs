using UnityEngine;
using UnityEngine.UI;

// type alias, 
using GameData = System.Tuple<byte, byte, byte, float, System.Collections.Generic.List<PieceInfo>>;

// type alias, (# of players, # of rows, # of cols, piece resolution)
using DimensionsData = System.Tuple<byte, byte, byte, byte>;
using System;
using System.Collections.Generic;


// Items displayed on the MakeGame canvas 
internal sealed class MakeGame : IAssociatedState<DimensionsData, GameData>,
    IAssociatedState<BoardInfo, GameData>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button makePieceButton;
    [SerializeField] internal Button makeBoardButton;
    [SerializeField] internal Button makeRuleButton;
    [SerializeField] internal Button setWinCondButton;
    [SerializeField] internal Button doneButton;
    [SerializeField] internal InputField nameInput;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeGame;
    }



    public void OnEnterState<G>(IAssociatedState<G, BoardInfo> prevState, BoardInfo boardMade) 
    { 
        // TODO
    }
    public void OnEnterState<G>(IAssociatedState<G, DimensionsData> prevState, DimensionsData data)
    {
        // TODO
    }


    public GameData OnLeaveState<G>(IAssociatedState<GameData, G> nextState)
    {
        // TODO
        return null;
    }
}
