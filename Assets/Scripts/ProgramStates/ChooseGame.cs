using UnityEngine;
using UnityEngine.UI;

// Items associated with the ChooseGame canvas
internal sealed class ChooseGame : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal ScrollRect chooseGameScrView;
    [SerializeField] internal Button deleteAllGamesButton;
    [SerializeField] internal Text warningText;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseGame;
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
