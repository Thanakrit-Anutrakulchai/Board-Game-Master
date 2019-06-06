using UnityEngine;
using UnityEngine.UI;

// Items displayed while playing a custom game
internal sealed class PlayGame : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Text curPlayerText;
    [SerializeField] internal ScrollRect movesScrView;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.PlayGame;
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
