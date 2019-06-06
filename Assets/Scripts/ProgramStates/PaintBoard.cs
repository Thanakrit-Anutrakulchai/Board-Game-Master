using UnityEngine;
using UnityEngine.UI;

// Items associated with paiting a board
internal sealed class PaintBoard : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;
    //TODO

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.PaintBoard;
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
