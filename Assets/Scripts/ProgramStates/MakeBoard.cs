using UnityEngine;
using UnityEngine.UI;

// Items for the board creation process
internal sealed class MakeBoard : IAssociatedState
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button removePieceButton;
    [SerializeField] internal ScrollRect selectPieceScrView;
    [SerializeField] internal Slider gapSlider;
    [SerializeField] internal Slider zoomSlider;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeBoard;
    }
}
