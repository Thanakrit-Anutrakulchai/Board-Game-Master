using UnityEngine;
using UnityEngine.UI;

// Items displayed on the MakeGame canvas 
internal sealed class MakeGame : IAssociatedState<Object, Object>
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

    public void OnEnterState(Object args)
    {
        // TODO
    }
    public Object OnLeaveState()
    {
        // TODO
        return null;
    }
}
