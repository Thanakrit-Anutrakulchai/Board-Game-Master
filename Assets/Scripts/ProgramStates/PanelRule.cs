using UnityEngine;
using UnityEngine.UI;


internal sealed class PanelRule : Process<PanelRule>, IAssociatedState<Object, Object>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;
    // TODO





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.PanelRule;
    }



    public void OnEnterState(IAssociatedStateLeave<Object> previousState, Object args)
    {
        throw new System.NotImplementedException();
    }



    public Object OnLeaveState(IAssociatedStateEnter<Object> nextState)
    {
        throw new System.NotImplementedException();
    }
}
