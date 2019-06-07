using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// type alias
using GameData = System.Tuple<byte, byte, byte, float, System.Collections.Generic.List<PieceInfo>>;

// Items for the board creation process
internal sealed class MakeBoard : IAssociatedState<GameData, BoardInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button doneButton;
    [SerializeField] internal Button pieceButtonTemplate;
    [SerializeField] internal Button removePieceButton;
    [SerializeField] internal ScrollRect selectPieceScrView;
    [SerializeField] internal Slider zoomSlider;





    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.MakeBoard;
    }



    /// <summary>
    /// Activates at the start of the make board process - sets editable board 
    /// up and populates selection scroll view with clickable buttons, among 
    /// other things.
    /// </summary>
    /// <param name="gamedata">tuple containing (# of rows, # of columns,  
    /// piece resolution, relative size of gap between squares) </param>
    public void OnEnterState<G>(IAssociatedState<G, GameData> _, GameData gamedata)
    {
        // get link to board handler
        BoardCreationHandler bh = BoardCreationHandler.GetHandler();

        // set up UIs on this canvas
        SetupUIs(bh);

        // unpacks information
        var (numRows, numCols, pceRes, gapSize, pieces) = gamedata;

        // clear old states and starts new board
        BoardInfo boardBeingMade = bh.StartNewBoard(numRows, numCols, gapSize);

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

                    // TODO TEMP DEBUG
                    // index of piece selected
                    Debug.Log("INDEX OF PIECE SELECTED: " + indexAssocPiece);

                    // notifies board creation handler
                    bh.PieceSelected = indexAssocPiece;
                });
        }


        // specifies and tiles a board 
        VirtualBoard<PieceSpawningSlot> virBoard = new VirtualBoard<PieceSpawningSlot>
            (
                boardBeingMade,
                pceRes,
                Prefabs.GetPrefabs().pieceSpawningSlot,
                (brd, r, c) => { bh.SetPiece(r, c); brd.RefreshSquare(r, c); }
            );
    }



    /// <summary>
    /// Destroys slots used to create the board and pass board information 
    /// back to the Make Game process
    /// </summary>
    /// <returns>The board created</returns>
    /// <param name="_">unused - next state (MakeGame)</param>
    /// <typeparam name="G">Dummy type variable</typeparam>
    public BoardInfo OnLeaveState<G>(IAssociatedState<BoardInfo, G> _)
    {
        // finish board creation
        BoardInfo createdBoard = BoardCreationHandler.GetHandler().FinishBoard();
        return createdBoard;
    }



    /// <summary>
    /// Ensures all User Interface on this canvas works during the process
    /// </summary>
    /// <param name="bh">Associated BoardCreationHandler</param>
    private void SetupUIs(BoardCreationHandler bh) 
    {
        removePieceButton.onClick.AddListener(
            delegate
            {
                // sets piece selected at start to no piece
                bh.PieceSelected = PieceInfo.noPiece;

                // changes background of all buttons in the scrollview to white
                selectPieceScrView.ForEach<Button>
                    (
                        (b) => b.GetComponent<Image>().color = Color.white
                    );

                // change colour of 'no piece' button 
                removePieceButton.GetComponent<Image>().color =
                    BoardCreationHandler.selectedPieceColour;
            });
    }
}
