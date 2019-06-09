using UnityEngine;
using UnityEngine.UI;

// Items displayed while playing a custom game
internal sealed class PlayGame : Process<PlayGame>, IAssociatedState<Game, Object>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Text curPlayerText;
    [SerializeField] internal readonly ScrollRect movesScrView;





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.PlayGame;
    }



    public void OnEnterState(IAssociatedStateLeave<Game> previousState, Game args)
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
