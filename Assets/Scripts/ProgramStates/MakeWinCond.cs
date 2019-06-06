using UnityEngine;
using UnityEngine.UI;

// Items associated with making a win condition
internal sealed class MakeWinCond : IAssociatedState
{
    [SerializeField] internal Canvas canvas;
    //TODO

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeWinCond;
    }
}
