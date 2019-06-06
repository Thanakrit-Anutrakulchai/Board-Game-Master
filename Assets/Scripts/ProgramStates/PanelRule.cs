using UnityEngine;
using UnityEngine.UI;


internal sealed class PanelRule : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;
    // TODO

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.PanelRule;
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
