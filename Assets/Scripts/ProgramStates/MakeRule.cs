using System;
using UnityEngine;
using UnityEngine.UI;

// typealias, (size, player who can use rule, player going after rule) : Tuple<byte, byte, byte>
using RuleSetupData = System.Tuple<byte, byte, byte>;


// items for the rule creation process
internal sealed class MakeRule : Process<MakeRule>, 
    IAssociatedState<RuleSetupData, RuleInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button allPiecesButton;
    [SerializeField] internal Button doneButton;
    [SerializeField] internal Button noPieceButton;
    [SerializeField] internal Button removePieceButton;
    [SerializeField] internal Button unaffectedButton;
    [SerializeField] internal Button pieceButtonTemplate;
    [SerializeField] internal Button setTriggerPieceButton;
    [SerializeField] internal Button toggleBeforeAfterButton;
    [SerializeField] internal Button togglePanelTriggerButton;
    [SerializeField] internal InputField nameInput;
    [SerializeField] internal ScrollRect selectPieceScrView;



    
    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeRule;
    }
     


    public void OnEnterState(IAssociatedStateLeave<RuleSetupData> previousState, RuleSetupData setupData)
    {
        // TODO
        RuleCreationHandler ruleHandler = RuleCreationHandler.GetHandler();

        // unpacks data
        (byte areaSize, byte playerUsing, byte nextPlayer) = setupData;

        SetupUIs();
        ruleHandler.StartNewRule(areaSize, playerUsing, nextPlayer);
    }



    public RuleInfo OnLeaveState(IAssociatedStateEnter<RuleInfo> nextState)
    {
        RuleCreationHandler ruleHandler = RuleCreationHandler.GetHandler();

        // destroys board displayed
        if (ruleHandler.SettingBoardAfter) 
        {
            ruleHandler.HoloBoardAfter.DestroyBoard();
        }
        else 
        {
            ruleHandler.HoloBoardBefore.DestroyBoard();
        }

        // TODO add checks
        string rlNm = nameInput.GetComponentInChildren<Text>().text;
        RuleInfo ruleMade = ruleHandler.FinalizeRule(rlNm);
        return ruleMade;
    }



    private void SetupUIs() 
    {
        RuleCreationHandler ruleHandler = RuleCreationHandler.GetHandler();

        // TODO this only needs to be setup the first time the screen is used
        // makes scrollview highlight chosen item (only)
        selectPieceScrView.WhenChosenChanges
            ((scrView) =>
                delegate
                {
                    // unhighlights other buttons
                    scrView.ForEach<Button>
                    ((btn) => 
                        {
                            btn.GetComponent<Image>().color = Color.white;
                        }
                    );

                    removePieceButton.GetComponent<Image>().color = Color.white;
                    setTriggerPieceButton.GetComponent<Image>().color = Color.white;
                    allPiecesButton.GetComponent<Image>().color = Color.white;
                    noPieceButton.GetComponent<Image>().color = Color.white;
                    unaffectedButton.GetComponent<Image>().color = Color.white;

                    // highlights chosen
                    if (scrView.GetChosenItem<Button>(out Button chosen) && 
                        chosen != null)
                    {
                        chosen.GetComponent<Image>().color =
                            RuleCreationHandler.selectedPieceColour;
                    }
                    else 
                    {
                        // TODO
                    }
                }
            );

        // clear old buttons
        selectPieceScrView.Clear(pieceButtonTemplate);

        // populates scroll view
        GameCreationHandler gameHandler = GameCreationHandler.GetHandler();

        // populates the scroll view with buttons labeled with piece names
        for (byte index = 0; index < gameHandler.pieces.Count; index++)
        {
            // index of the associated piece 
            //  index should not be used directly in delegate, as it *will* change
            //  after this iteration of the loop ends
            // index and indexAssocPiece are kind of like up'value's in Lua
            byte indexAssocPiece = index;
            PieceInfo pce = gameHandler.pieces[index];

            // creates a button tagged with the piece name and attach it to scrollView
            Button pceButton =
                Utility.CreateButton(pieceButtonTemplate, selectPieceScrView.content,
                pce.pieceName,
                (btn) => delegate
                {
                    selectPieceScrView.SetChosenItem(btn);

                    // TODO TEMP DEBUG
                    // index of piece selected
                    Debug.Log("INDEX OF PIECE SELECTED: " + indexAssocPiece);

                    // notifies board creation handler
                    ruleHandler.PieceSelected =
                        new RuleCreationHandler
                                .PieceSelection
                                .OnePiece(indexAssocPiece);
                               
                });
        }


        // places all the pieces on square clicked
        allPiecesButton.onClick.AddListener
            (delegate
            {
                ruleHandler.PieceSelected =
                    new RuleCreationHandler.PieceSelection
                                           .AllPieces();

                // highlights button
                selectPieceScrView.SetChosenItem(allPiecesButton);

            });

        // only applicable if there is NOT a piece there
        noPieceButton.onClick.AddListener
            (delegate
            {
                ruleHandler.PieceSelected =
                    new RuleCreationHandler.PieceSelection
                                           .NoPiece();

                selectPieceScrView.SetChosenItem(noPieceButton);
            });

        // change piece selected when clicked
        removePieceButton.onClick.AddListener
            (delegate 
            {
                ruleHandler.PieceSelected =
                    new RuleCreationHandler.PieceSelection
                                           .RemovePiece();

                // highlights button
                selectPieceScrView.SetChosenItem(removePieceButton);
            });

        // notifies handler and change button colour
        setTriggerPieceButton.onClick.AddListener
            (
                delegate 
                {
                    // toggle state
                    ruleHandler.SelectingTriggerPiece = 
                        !ruleHandler.SelectingTriggerPiece;

                    // unhighlights other button 
                    selectPieceScrView.SetChosenItem(setTriggerPieceButton);

                    // colour according to state
                    if (ruleHandler.SelectingTriggerPiece) 
                    {
                        setTriggerPieceButton.GetComponent<Image>().color =
                            RuleCreationHandler.setTriggerTrueColour;
                    }
                    else 
                    {
                        setTriggerPieceButton.GetComponent<Image>().color =
                            RuleCreationHandler.setTriggerFalseColour;
                    }
                }
            );

        // switches between specifying the before and the after state of the area
        toggleBeforeAfterButton.onClick.AddListener
            (
                delegate 
                {
                    // toggle between setting before/setting after
                    ruleHandler.SettingBoardAfter =
                        !ruleHandler.SettingBoardAfter;

                    if (ruleHandler.SettingBoardAfter) // if setting after state
                    {
                        toggleBeforeAfterButton.GetComponentInChildren<Text>().text =
                            "SET AREA BEFORE";
                    }
                    else // if setting before state
                    {
                        toggleBeforeAfterButton.GetComponentInChildren<Text>().text =
                            "SET AREA AFTER"; 
                    }
                }
            );

        // switches between panel rules and trigger rules
        togglePanelTriggerButton.onClick.AddListener
            (
                delegate
                {
                    // toggle 
                    ruleHandler.MakingTriggerRule =
                        !ruleHandler.MakingTriggerRule;

                    if (ruleHandler.MakingTriggerRule) 
                    {
                        togglePanelTriggerButton.GetComponentInChildren<Text>().text =
                            "CHANGE TO PANEL RULE";
                    }
                    else 
                    {
                        togglePanelTriggerButton.GetComponentInChildren<Text>().text =
                            "CHANGE TO TRIGGER RULE";
                    }
                }
            );

        // changes square selected to become unaffected
        unaffectedButton.onClick.AddListener
            (delegate
            {
                ruleHandler.PieceSelected =
                    new RuleCreationHandler.PieceSelection
                                           .Unaffected();

                selectPieceScrView.SetChosenItem(unaffectedButton);
            });
    }
}
