using UnityEngine;
using UnityEngine.UI;

// items for the rule creation process
internal sealed class MakeRule : IAssociatedState
{
    [SerializeField] internal Canvas canvas;
    //TODO

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeRule;
    }
}
