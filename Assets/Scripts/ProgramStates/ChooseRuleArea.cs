using UnityEngine;
using UnityEngine.UI;

// Items associated with specifying size of area affected by rule and player's turn
internal sealed class ChooseRuleArea : IAssociatedState
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal InputField areaSizeInput;
    [SerializeField] internal InputField playerTurnInput;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseRuleArea;
    }
}
