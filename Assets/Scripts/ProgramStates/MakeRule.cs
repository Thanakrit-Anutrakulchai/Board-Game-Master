using UnityEngine;
using UnityEngine.UI;

// items for the rule creation process
internal sealed class MakeRule : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;
    //TODO

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeRule;
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
