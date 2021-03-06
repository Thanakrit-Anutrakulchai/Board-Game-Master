﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// type alias, (size of winning structure in squares) : Tuple<byte>
using WinCondSetupData = System.Tuple<byte, byte>;

// Items associated with making a win condition
internal sealed class MakeWinCond : Process<MakeWinCond>, 
    IAssociatedState<WinCondSetupData, WinCondInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button anythingButton;
    [SerializeField] internal Button doneButton;
    [SerializeField] internal Button pieceButtonTemplate;
    [SerializeField] internal Button noPieceButton;
    [SerializeField] internal Button removePieceButton;
    [SerializeField] internal InputField nameInput;
    [SerializeField] internal ScrollRect selectPieceScrView;
    [SerializeField] internal Text complainText;
    [SerializeField] internal Slider zoomSlider;





    /*** START ***/
    private void Start()
    {
        zoomSlider.onValueChanged.AddListener
            (
                (h) => CameraHandler.GetHandler().MoveCamera(h)
            );

        // sets up chosen button highlighting
        selectPieceScrView.WhenChosenChanges
            ((scrView) => 
                delegate
                {
                    scrView.HighlightOnlyChosen<Button>
                    (
                        new List<Button> { removePieceButton },
                        WinCondCreationHandler.selectedPieceColour
                    );
                }
            );

        // highlights and set piece selected for buttons outside scrollview
        removePieceButton.onClick.AddListener
            (delegate 
            {
                selectPieceScrView.SetChosenItem(removePieceButton);
                WinCondCreationHandler winHandler = WinCondCreationHandler.GetHandler();
                winHandler.pieceSelected = PieceInfo.noPiece;
            });

        // highlights and say square click is not checked 
        anythingButton.onClick.AddListener
            (delegate
            {
                selectPieceScrView.SetChosenItem(anythingButton);
                WinCondCreationHandler winHandler = WinCondCreationHandler.GetHandler();
                winHandler.pieceSelected = PieceInfo.noSquare;
            });
    }





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeWinCond;
    }



    public void OnEnterState(IAssociatedStateLeave<WinCondSetupData> previousState, 
                             WinCondSetupData data)
    {
        SetupUIs();

        // unpacks data
        (byte size, byte winner) = data;

        // starts creation process
        WinCondCreationHandler winCondHandler = WinCondCreationHandler.GetHandler();
        winCondHandler.StartNewWinCond(size, winner);
    }



    public WinCondInfo OnLeaveState(IAssociatedStateEnter<WinCondInfo> nextState)
    {
        bool validInput = Utility.EnsureProperName(nameInput.text);
        if (validInput)
        {
            WinCondCreationHandler winCondHandler = WinCondCreationHandler.GetHandler();
            WinCondInfo winCondMade = winCondHandler.FinalizeWinCond(nameInput.text);
            return winCondMade;
        }
        else 
        {
            complainText.text =
                "Name must contain only digits, letters, and spaces";


            TransitionHandler.GetHandler().AbortTransition();
            return null;
        }


    }



    private void SetupUIs() 
    {
        complainText.text = "";

        // clear old name
        nameInput.text = "";


        // repopulates scroll view with piece buttons
        WinCondCreationHandler winHandler = WinCondCreationHandler.GetHandler();
        selectPieceScrView.Clear(pieceButtonTemplate);
        selectPieceScrView.RepopulatePieceButtons
            (
                pieceButtonTemplate,
                (btn, index) => winHandler.pieceSelected = index
            );
    }
}
