using UnityEngine;
using UnityEngine.UI;

// Items displayed on the Intro canvas
internal sealed class Intro : Process<Intro>, IAssociatedState<GameInfo, Object>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Button playGameButton;
    [SerializeField] internal readonly Button makeGameButton;






    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.Intro;
    }



    public void OnEnterState(IAssociatedStateLeave<GameInfo> previousState, GameInfo args)
    {
        // TODO
        throw new System.NotImplementedException();
    }



    public Object OnLeaveState(IAssociatedStateEnter<Object> nextState)
    {
        // TODO
        throw new System.NotImplementedException();
    }
}
