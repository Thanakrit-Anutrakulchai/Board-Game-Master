using System;
using UnityEngine;
using UnityEngine.UI;

// typealias, (size, player #'s turn) : Tuple<byte, byte>
using RuleSetupData = System.Tuple<byte, byte>;


// items for the rule creation process
internal sealed class MakeRule : Process<MakeRule>, 
    IAssociatedState<RuleSetupData, RuleInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Button doneButton;
    [SerializeField] internal readonly Button removePieceButton;
    [SerializeField] internal readonly Button setTriggerPieceButton;
    [SerializeField] internal readonly Button toggleBeforeAfterButton;
    [SerializeField] internal readonly Button togglePanelTriggerButton;
    [SerializeField] internal readonly ScrollRect selectPieceScrView;




    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeRule;
    }



    public void OnEnterState(IAssociatedStateLeave<RuleSetupData> previousState, RuleSetupData args)
    {
        // TODO
        throw new NotImplementedException();
    }



    public RuleInfo OnLeaveState(IAssociatedStateEnter<RuleInfo> nextState)
    {
        // TODO
        throw new NotImplementedException();
    }
}
