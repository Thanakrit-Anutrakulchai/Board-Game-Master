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
        // Add handlers to buttons which transitions between states 
        Intro.playGameButton.onClick.AddListener
            (
                () => Transition(Intro, PlayGame)
            );

        Intro.makeGameButton.onClick.AddListener
            (
                () => Transition(Intro, ChooseBoardDim)
            );

        ChooseBoardDim.useDimsButton.onClick.AddListener
            (
                () => Transition(ChooseBoardDim, MakeGame)
            );

        MakeGame.doneButton.onClick.AddListener
            (
                () => Transition(MakeGame, Intro)
            );

        MakeGame.makeBoardButton.onClick.AddListener
            (
                () => Transition(MakeGame, MakeBoard)
            );

        MakeGame.makePieceButton.onClick.AddListener
            (
                () => Transition(MakeGame, MakePiece)
            );

        MakeGame.makeRuleButton.onClick.AddListener
            (
                () => Transition(MakeGame, ChooseRuleArea) // TODO to change
            );

        MakeBoard.doneButton.onClick.AddListener
            (
                () => Transition(MakeBoard, MakeGame)
            );

        MakePiece.doneButton.onClick.AddListener
            (
                () => Transition(MakePiece, MakeGame)
            );



        // TODO Move these to the ProgramStates classes


        deleteAllGamesButton.onClick.AddListener(DeleteAllGames);

        activatePieceClickedButton.onClick.AddListener(MakeRelRule);
        setTriggerPieceButton.onClick.AddListener(SetTriggerPiece);
        relRuleSwitchToButton.onClick.AddListener(RelRuleSwitchTo);
        relRuleDoneButton.onClick.AddListener(DoneRelRule);
    }



    // TODO
    // assigns action to all scroll views when chosen item is changed
    internal void SetupScrollViews() 
    {
        MakeBoard.selectPieceScrView.WhenChosenChanges
            ((scrView) => delegate
                {
                    // selects all non-highlighted buttons background to white
                    scrView.ForEach<Button>(
                        (b) => b.GetComponent<Image>().color = Color.white);

                    // including remove piece button
                    MakeBoard.removePieceButton.GetComponent<Image>().color = 
                        Color.white;

                    // highlights chosen button
                    if (scrView.GetChosenItem<Button>(out Button chosen) && 
                        chosen != null) 
                    {
                        chosen.GetComponent<Image>().color = 
                            BoardCreationHandler.selectedPieceColour;
                    }
                }
            );
    }



    // transition states: switch canvas, update states, calls OnEnter/OnLeave methods
    private void Transition<S, T, R>(IAssociatedState<S, T> prev, IAssociatedState<T, R> next) 
    {
        // update state
        ProgramData.currentState = next.GetAssociatedState();

        // call the corresponding method before leaving state
        T args = prev.OnLeaveState(next);

        // switch canvas
        prev.GetCanvas().gameObject.SetActive(false);
        next.GetCanvas().gameObject.SetActive(true);

        // move the camera to the pre-specified location
        Camera.main.transform.position = SpatialConfigs.commonCameraPosition;

        // call corresponding method upon entering state, 
        //   passes arguments returned from previous state
        next.OnEnterState(prev, args);
    }
}
