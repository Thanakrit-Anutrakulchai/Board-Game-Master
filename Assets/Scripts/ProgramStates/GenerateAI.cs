using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


internal class GenerateAI : Process<GenerateAI>,
    IAssociatedState<Game, List<BotInfo>>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    //[SerializeField] internal Button setupAIsButton;
    [SerializeField] internal Text pleaseWaitText;







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
        SetupUIs();
        AIGenerationHandler.GetHandler().GenerateBots(game);
    }



    // GenerateAI -> Choose Game
    public List<BotInfo> OnLeaveState(IAssociatedStateEnter<List<BotInfo>> nextState)
    {
        return AIGenerationHandler.GetHandler().RetrieveBots();
    }



    // reset wait text, hides setup ai button
    private void SetupUIs() 
    {
        pleaseWaitText.text = AIGenerationHandler.defaultWaitText;
        //setupAIsButton.gameObject.SetActive(false);
    }




}