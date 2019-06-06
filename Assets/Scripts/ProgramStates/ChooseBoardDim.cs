using UnityEngine;
using UnityEngine.UI;

// Items for entering the dimensions (of board) specification process
internal sealed class ChooseBoardDim : IAssociatedState<Object, Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal InputField numPlayersInput;
    [SerializeField] internal InputField numRowsInput;
    [SerializeField] internal InputField numColsInput;
    [SerializeField] internal InputField pceResInput;
    [SerializeField] internal Button useDimsButton;



    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseBoardDim;
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
