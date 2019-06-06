using UnityEngine;
using UnityEngine.UI;

// Items displayed on the Intro canvas
internal sealed class Intro : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button playGameButton;
    [SerializeField] internal Button makeGameButton;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.Intro;
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
