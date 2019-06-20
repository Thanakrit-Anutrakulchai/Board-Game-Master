using System;
using UnityEngine;
using UnityEngine.UI;

// typealias, (size, player who can use rule, player going after rule) : Tuple<byte, byte, byte>
using RuleSetupData = System.Tuple<byte, byte, byte>;


// Items associated with specifying size of area affected by rule and player's turn
internal sealed class ChooseRuleArea : Process<ChooseRuleArea>, 
    IAssociatedState<GameCreationHandler, RuleSetupData>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button startMakingRuleButton;
    [SerializeField] internal InputField areaSizeInput;
    [SerializeField] internal InputField whichPlayerUseInput;
    [SerializeField] internal InputField whoseTurnAfterInput;
    [SerializeField] internal Text complainText;






    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseRuleArea;
    }



    public void OnEnterState(IAssociatedStateLeave<GameCreationHandler> previousState, 
                             GameCreationHandler gameHandler)
    {
        SetupUIs();
    }



    public RuleSetupData OnLeaveState(IAssociatedStateEnter<RuleSetupData> nextState)
    {
        GameCreationHandler gameHandler = GameCreationHandler.GetHandler();

        // TODO allow rectangular (non-square) affected area
        byte maxAreaSize = Math.Min(gameHandler.NumOfRows, gameHandler.NumOfCols);

        bool validInput = byte.TryParse(areaSizeInput.text, out byte areaSize) &&
            areaSize.InRange(1, maxAreaSize);
        validInput &= byte.TryParse(whichPlayerUseInput.text, out byte playerUsing) && 
            playerUsing.InRange(1, gameHandler.numOfPlayers);
        validInput &= byte.TryParse(whoseTurnAfterInput.text, out byte playerAfter) && 
            playerAfter.InRange(1, gameHandler.numOfPlayers);

        if (validInput)
        {
            return Tuple.Create(areaSize, (byte) (playerUsing - 1), (byte) (playerAfter - 1));
        }
        else 
        {
            complainText.text = 
                "Please enter valid whole numbers";

            TransitionHandler.GetHandler().AbortTransition();

            return null; 
        }
    }



    // clears old data in input fields
    private void SetupUIs() 
    {
        areaSizeInput.text = "";
        whichPlayerUseInput.text = "";
        whoseTurnAfterInput.text = "";

        complainText.text = "";
    }
}
