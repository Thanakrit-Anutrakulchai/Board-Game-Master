using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// screen for choosing which turns will be played by an AI
internal class SetupAIs : Process<SetupAIs>,
    IAssociatedState<List<BotInfo>, Dictionary<byte, BotInfo>> 
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button doneButton;
    [SerializeField] internal Button setAIButtonTemplate;
    [SerializeField] internal ScrollRect setAIsScrView;







    /*** INSTANCE METHODS ***/
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.SettingAIs;
    }



    public Canvas GetCanvas()
    {
        return canvas;
    }



    public void OnEnterState(IAssociatedStateLeave<List<BotInfo>> previousState, List<BotInfo> bots)
    {
        SetupUIs(bots[0].game, bots);
        SetupAIsHandler setAIs = SetupAIsHandler.GetHandler();
        setAIs.NewBotsSetup();
    }



    public Dictionary<byte, BotInfo> OnLeaveState(IAssociatedStateEnter<Dictionary<byte, BotInfo>> nextState)
    {
        SetupAIsHandler setAIs = SetupAIsHandler.GetHandler();
        return setAIs.FinalizeBotsSetup();
    }



    // clears and repopulates scroll view
    private void SetupUIs(GameInfo game, List<BotInfo> bots) // note that this takes in 1 Game argument
    {
        setAIsScrView.RepopulateButtons
            (
                setAIButtonTemplate,
                game.playerNames,
                (btn, player) =>
                {
                    SetupAIsHandler setAIs = SetupAIsHandler.GetHandler();

                    // toggle
                    if (setAIs.botsSetup.ContainsKey(player)) 
                    {
                        btn.GetComponent<Image>().color = Color.white;
                        setAIs.botsSetup.Remove(player);
                    } 
                    else 
                    {
                        btn.GetComponent<Image>().color =
                            SetupAIsHandler.selectedPlayerColour;
                        setAIs.botsSetup.Add(player, bots[player]);
                    }
                }
            );
    }





}
