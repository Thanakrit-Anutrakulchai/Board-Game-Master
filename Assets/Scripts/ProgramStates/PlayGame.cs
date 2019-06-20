using UnityEngine;
using UnityEngine.UI;

// Items displayed while playing a custom game
internal sealed class PlayGame : Process<PlayGame>, IAssociatedState<Game, Object>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button moveButtonTemplate;
    [SerializeField] internal Button quitGameButton;
    [SerializeField] internal ScrollRect movesScrView;
    [SerializeField] internal Text curPlayerText;
    [SerializeField] internal Text winnerText;





    /*** START ***/
    // adds listeners
    private void Start()
    {
        //
    }





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.PlayGame;
    }



    public void OnEnterState(IAssociatedStateLeave<Game> previousState, Game game)
    {
        SetupUIs();

        GamePlayHandler gameHandler = GamePlayHandler.GetHandler();
        gameHandler.StartGame(game);
    }



    public Object OnLeaveState(IAssociatedStateEnter<Object> nextState)
    {
        GamePlayHandler gameHandler = GamePlayHandler.GetHandler();
        gameHandler.QuitGame();
        return null;
    }



    // clears moves scroll view, reset player text
    private void SetupUIs() 
    {
        // clear scroll view
        movesScrView.Clear(moveButtonTemplate);

        // resets current player text, just incase
        curPlayerText.text = "";

        // resets winning player
        winnerText.text = "";
    }
}
