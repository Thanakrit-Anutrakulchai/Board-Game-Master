using System;
using UnityEngine;


internal class GenerateAI : Process<GenerateAI>,
    IAssociatedState<Game, BotInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;







    /*** INSTANCE METHODS ***/
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.GeneratingAI;
    }



    public Canvas GetCanvas()
    {
        return canvas;
    }


    // Choose Game -> GenerateAI
    public void OnEnterState(IAssociatedStateLeave<Game> previousState, Game game)
    {
        throw new NotImplementedException();
    }



    // GenerateAI -> Choose Game
    public BotInfo OnLeaveState(IAssociatedStateEnter<BotInfo> nextState)
    {
        throw new NotImplementedException();
    }




}