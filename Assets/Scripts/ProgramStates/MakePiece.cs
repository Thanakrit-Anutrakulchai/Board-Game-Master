using UnityEngine;
using UnityEngine.UI;

// Items for piece creation process
internal sealed class MakePiece : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button doneButton;
    [SerializeField] internal InputField nameInput;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakePiece;
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
