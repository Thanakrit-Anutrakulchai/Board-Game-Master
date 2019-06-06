using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Items for the board creation process
internal sealed class MakeBoard : 
    IAssociatedState<Tuple<byte, byte, byte, List<PieceInfo>>, UnityEngine.Object>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button doneButton;
    [SerializeField] internal Button pieceButtonTemplate;
    [SerializeField] internal Button removePieceButton;
    [SerializeField] internal ScrollRect selectPieceScrView;
    [SerializeField] internal Slider gapSlider;
    [SerializeField] internal Slider zoomSlider;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeBoard;
    }

    public void OnEnterState(Tuple<byte, byte, byte, List<PieceInfo>> gamedata)
    {
        // get link to board handler
        BoardCreationHandler bh = BoardCreationHandler.GetHandler();

        // clear old states
        bh.StartNewBoard();

        // unpacks information
        var (numRows, numCols, pceRes, pieces) = gamedata;

        // clears all old visible button on scroll view
        selectPieceScrView.Clear(pieceButtonTemplate);

        // populates the scroll view with buttons labeled with piece names
        for (byte index = 0; index < pieces.Count; index++)
        {
            // index of the associated piece 
            //  index should not be used directly in delegate, as it *will* change
            //  after this iteration of the loop ends
            // index and indexAssocPiece are kind of like up'value's in Lua
            byte indexAssocPiece = index;
            PieceInfo pce = pieces[index];

            // creaets a button tagged with the piece name and attach it to scrollView
            Button pceButton =
                Utility.CreateButton(pieceButtonTemplate, selectPieceScrView.content,
                pce.pieceName,
                (btn) => delegate
                {
                    selectPieceScrView.SetChosenItem(btn);

                    // TODO 
                    // TEMP
                    // DEBUG
                    // index of piece selected
                    Debug.Log("INDEX OF PIECE SELECTED: " + indexAssocPiece);

                    // notifies board creation handler
                    bh.PieceSelected = indexAssocPiece;
                });


        }
    }
    public UnityEngine.Object OnLeaveState()
    {
        // TODO
        return null;
    }
}
