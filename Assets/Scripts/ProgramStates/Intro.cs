using UnityEngine;
using UnityEngine.UI;

// Items displayed on the Intro canvas
internal sealed class Intro : IAssociatedState
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button playGameButton;
    [SerializeField] internal Button makeGameButton;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.Intro;
    }
}
