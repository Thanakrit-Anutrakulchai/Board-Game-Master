using UnityEngine;
using UnityEngine.UI;

// Items displayed during the relative rule creation process
internal sealed class RelativeRule : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button doneButton;
    [SerializeField] internal Button removePieceButton;
    [SerializeField] internal Button setTriggerPieceButton;
    [SerializeField] internal ScrollRect selectPieceScrView;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.RelativeRule;
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
