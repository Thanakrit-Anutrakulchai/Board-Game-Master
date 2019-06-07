using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// A class for handling boards tiled by spawning slots
/// </summary>
public class VirtualBoard<Slot> : MonoBehaviour where Slot : PieceSlot
{
    /*** INSTANCE VARIABLES ***/
    /// <summary>
    /// The PieceSlots used to tile this board <para />
    /// Each list corresponds to slots used to tile each board square
    /// </summary>
    private readonly List<Slot>[,] slots;

    /// <summary>
    /// PieceSlot game object used as instantiation template for making
    /// more slots.
    /// </summary>
    private readonly Slot slotTemplate; 

    /// <summary>
    /// provides information about the board 
    /// </summary>
    private BoardInfo info;
    private byte slotsPerSide; // normally this would correspond to pieceResolution

    /// <summary>
    /// Handler which is called whenever a board square is clicked.
    /// The positions (index) of the square clicked is passed in, as well,
    /// starting from (r=0, c=0) on the bottom left and increasing up and rightwards
    /// </summary>
    private readonly UnityAction<VirtualBoard<Slot>, byte, byte> onClickHandler;





    /*** INSTANCE PROPERTIES ***/
    internal BoardInfo Info { get; }





    /*** CONSTRUCTOR ***/
    internal VirtualBoard(BoardInfo startBoard, byte subsq, Slot template,
                          UnityAction<VirtualBoard<Slot>, byte, byte> handler) 
    {
        info = startBoard;
        slotsPerSide = subsq;
        slotTemplate = template;
        onClickHandler = handler;
    }





    /*** INSTANCE METHODS ***/
    /// <summary>
    /// Calls the OnCreate method of the slots tiling the square at the 
    /// position specified
    /// </summary>
    /// <param name="row">row the square is in</param>
    /// <param name="col">column the square is in</param>
    internal void RefreshSquare(byte row, byte col)
    {
        slots[row, col].ForEach((s) => s.OnCreate());
    }



    /// <summary>
    /// Spawns the board (slots) into the game world according to the 
    /// board info specified during construction.
    /// </summary>
    /// <param name="centrePos">position of the centre of the board</param>
    internal void SpawnBoard(Vector3 centrePos) 
    {
        Vector3 start = info.BottomLeft + centrePos;

        // tiles and assigns appropriate variables to piece spawning slots
        float slotSize = (info.SquareSize / 10) / slotsPerSide;
        Utility.TileAct(start, slotTemplate.gameObject, slotSize,
            info.NumOfRows, info.NumOfCols, slotsPerSide,
            info.SizeOfGap,
            (slot, boardR, boardC, pieceR, pieceC) =>
            {
                // assigns variables
                Slot slotScr =
                    slot.GetComponent<Slot>();
                slotScr.pieceRow = pieceR;
                slotScr.pieceCol = pieceC;
                slotScr.boardRow = boardR;
                slotScr.boardCol = boardC;

                // notes that this spawning slot is currently used
                slots[boardR, boardC].Add(slotScr);

                // spawns cube at the corresponding position relative to the piece
                slotScr.OnCreate();

                // adds object to list of item to destroy after creation process
                Utility.objsToDelete.Add(slot);
            });
    }


    /// <summary>
    /// Destroy all of the piece slots used in this board
    /// </summary>
    internal void DestroyBoard() 
    { 
        foreach (List<Slot> sq in slots) 
        {
            sq.ForEach((obj) => Destroy(obj.gameObject));
        }
    }
}
