using UnityEngine;
using UnityEngine.UI;

// class which handles the transitions between different processes and canvases
public class TransitionHandler
{
    /*** STATIC VARIABLES ***/
    // the state of the program 
    internal ProgramData programState;





    /*** INSTANCE VARIABLESx ***/
    /** Unity Objects **/
    private readonly ChooseBoardDim ChooseBoardDim = Camera.main.GetComponent<ChooseBoardDim>();
    private readonly ChooseGame ChooseGame = Camera.main.GetComponent<ChooseGame>();
    private readonly ChooseRuleArea ChooseRuleArea = Camera.main.GetComponent<ChooseRuleArea>();
    private readonly Intro Intro = Camera.main.GetComponent<Intro>();
    private readonly MakeBoard MakeBoard = Camera.main.GetComponent<MakeBoard>();
    private readonly MakeGame MakeGame = Camera.main.GetComponent<MakeGame>();
    private readonly MakePiece MakePiece = Camera.main.GetComponent<MakePiece>();
    private readonly MakeRule MakeRule = Camera.main.GetComponent<MakeRule>();
    private readonly MakeWinCond MakeWinCond = Camera.main.GetComponent<MakeWinCond>();
    private readonly PaintBoard PaintBoard = Camera.main.GetComponent<PaintBoard>();
    private readonly PanelRule PanelRule = Camera.main.GetComponent<PanelRule>();
    private readonly PlayGame PlayGame = Camera.main.GetComponent<PlayGame>();
    private readonly RelativeRule RelativeRule = Camera.main.GetComponent<RelativeRule>();

    /* Et Cetera */
    private byte numTimesDeleteAllGamesClickedSinceDeletion;





    /*** INSTANCE METHODS ***/
    internal void AddListenersToButtons() 
    {
        // Add handlers to buttons   
        playGameButton.onClick.AddListener(PlayGame);


        makeGameButton.onClick.AddListener(MakeGame);
        useTheseDimsButton.onClick.AddListener(UseTheseDims);
        doneGameButton.onClick.AddListener(DoneGame);

        deleteAllGamesButton.onClick.AddListener(DeleteAllGames);

        makeBoardButton.onClick.AddListener(MakeBoard);
        removePieceButton.onClick.AddListener(RemovePiece);
        doneBoardButton.onClick.AddListener(DoneBoard);

        makePieceButton.onClick.AddListener(MakePiece);
        donePieceButton.onClick.AddListener(DonePiece);

        makeRuleButton.onClick.AddListener(MakeRule);
        activatePieceClickedButton.onClick.AddListener(MakeRelRule);
        setTriggerPieceButton.onClick.AddListener(SetTriggerPiece);
        relRuleSwitchToButton.onClick.AddListener(RelRuleSwitchTo);
        relRuleDoneButton.onClick.AddListener(DoneRelRule);
    }



    private void Transition(IAssociatedState prev, IAssociatedState next) 
    {
        // update state
        ProgramData.currentState = next.GetAssociatedState();

        // switch canvas
        prev.GetCanvas().gameObject.SetActive(false);
        next.GetCanvas().gameObject.SetActive(true);
    }
}
