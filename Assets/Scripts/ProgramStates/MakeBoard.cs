using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Items for the board creation process
internal sealed class MakeBoard : Process<MakeBoard>, IAssociatedState<GameCreationHandler, BoardInfo>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Button doneButton;
    [SerializeField] internal readonly Button pieceButtonTemplate;
    [SerializeField] internal readonly Button removePieceButton;
    [SerializeField] internal readonly ScrollRect selectPieceScrView;
    [SerializeField] internal readonly Slider zoomSlider;






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
    public void OnEnterState(IAssociatedStateLeave<GameCreationHandler> _,GameCreationHandler gamedata)
    {
        // get link to board handler
        BoardCreationHandler bh = BoardCreationHandler.GetHandler();

        // set up UIs on this canvas
        SetupUIs(bh);

        // unpacks information
        byte numRows = gamedata.NumOfRows;
        byte numCols = gamedata.NumOfCols;
        byte pceRes = gamedata.pieceResolution;
        float gapSize = gamedata.SizeOfGap;
        List<PieceInfo> pieces = gamedata.pieces;

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
                pieces,
                pceRes,
                Prefabs.GetPrefabs().pieceSpawningSlot,
                (brd, r, c) => bh.TogglePiece(r, c)
            );

        bh.VirtualBoardUsed = virBoard;
        virBoard.SpawnBoard(SpatialConfigs.commonBoardSpawn);
    }



    /// <summary>
    /// Destroys slots used to create the board and pass board information 
    /// back to the Make Game process
    /// </summary>
    /// <returns>The board created</returns>
    /// <param name="_">unused - next state (MakeGame)</param>
    /// <typeparam name="G">Dummy type variable</typeparam>
    public BoardInfo OnLeaveState(IAssociatedStateEnter<BoardInfo> _)
    {
        // finish board creation
        BoardCreationHandler bh = BoardCreationHandler.GetHandler();
        bh.VirtualBoardUsed.DestroyBoard();
        BoardInfo createdBoard = bh.FinalizeBoard();
        return createdBoard;
    }



    /// <summary>
    /// Ensures all User Interface on this canvas works during the process
    /// </summary>
    /// <param name="bh">Associated BoardCreationHandler</param>
    private void SetupUIs(BoardCreationHandler bh) 
    {
        // 
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

        // highlights button clicked on scroll view while resetting all others
        selectPieceScrView.WhenChosenChanges
            ((scrView) => delegate
            {
                // selects all non-highlighted buttons background to white
                scrView.ForEach<Button>(
                    (b) => b.GetComponent<Image>().color = Color.white);

                // including remove piece button
                removePieceButton.GetComponent<Image>().color =
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
}
