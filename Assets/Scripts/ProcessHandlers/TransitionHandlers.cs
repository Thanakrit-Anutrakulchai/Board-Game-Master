using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// type alias, (# of players, # of rows, # of cols, piece resolution)
using DimensionsData = System.Tuple<byte, byte, byte, byte>;


// class which handles the transitions between different processes and canvases
public class TransitionHandler : ProcessHandler<TransitionHandler>
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

    /* Et Cetera */
    private byte numTimesDeleteAllGamesClickedSinceDeletion;





 
    /*** INSTANCE METHODS ***/
    internal void AddListenersToButtons() 
    {
        // Add handlers to buttons which transitions between states 
        // NOTE: This really shows C#'s powerful type (inference) system
        Intro.playGameButton.onClick.AddListener
            (
                () => Transition(Intro, ChooseGame)
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
                () => Transition(MakeGame, ChooseRuleArea) 
            );

        MakeBoard.doneButton.onClick.AddListener
            (
                () => Transition(MakeBoard, MakeGame)
            );

        MakePiece.doneButton.onClick.AddListener
            (
                () => Transition(MakePiece, MakeGame)
            );

        ChooseRuleArea.startMakingRuleButton.onClick.AddListener
            (
                () => Transition(MakePiece, MakeGame)
            );



        // TODO Move these to the ProgramStates classes

        /*
        deleteAllGamesButton.onClick.AddListener(DeleteAllGames);

        activatePieceClickedButton.onClick.AddListener(MakeRelRule);
        setTriggerPieceButton.onClick.AddListener(SetTriggerPiece);
        relRuleSwitchToButton.onClick.AddListener(RelRuleSwitchTo);
        relRuleDoneButton.onClick.AddListener(DoneRelRule);
        */      
    }


    // gets the canvas associated with this state 
    private Canvas CanvasOf(ProgramData.State state) 
    { 
        switch (state) 
        {
            case ProgramData.State.ChooseBoardDim:
                return ChooseBoardDim.GetCanvas();
            case ProgramData.State.ChooseGame:
                return ChooseGame.GetCanvas();
            case ProgramData.State.ChooseRuleArea:
                return ChooseRuleArea.GetCanvas();
            case ProgramData.State.Intro:
                return Intro.GetCanvas();
            case ProgramData.State.MakeBoard:
                return MakeBoard.GetCanvas();
            case ProgramData.State.MakeGame:
                return MakeGame.GetCanvas();
            case ProgramData.State.MakePiece:
                return MakePiece.GetCanvas();
            case ProgramData.State.MakeRule:
                return MakeRule.GetCanvas();
            case ProgramData.State.MakeWinCond:
                return MakeWinCond.GetCanvas();
            case ProgramData.State.PaintBoard:
                return PaintBoard.GetCanvas();
            case ProgramData.State.PanelRule:
                return PanelRule.GetCanvas();
            case ProgramData.State.PlayGame:
                return PlayGame.GetCanvas();
            default:
                throw new System.Exception("Received unaccountedfor state");
        }
    }



    // creates a button which transitions between canvases and processes when clicked
    //   after applying the action specified
    internal void CreateTransitionButton<S, T, R>(Button template, Component location,
                                                  string text,
                                                  IAssociatedState<S, T> from,
                                                  IAssociatedState<T, R> to,
                                                  UnityAction actBeforeTransition) 
    {
        Button button = Utility.CreateButton(template, location, text, 
            delegate 
            {
                actBeforeTransition();
                Transition(from, to);
            });
    }



    // transition states: switch canvas, update states, calls OnEnter/OnLeave methods
    private void Transition<T>(IAssociatedStateLeave<T> prev, IAssociatedStateEnter<T> next) 
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
