using UnityEngine;
using UnityEngine.UI;

// Items associated with the ChooseGame canvas
internal sealed class ChooseGame : IAssociatedState
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
}
