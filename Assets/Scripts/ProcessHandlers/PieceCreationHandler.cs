using UnityEngine;

// script which controls the behaviour of the piece creation process 
public class PieceCreationHandler
{
    /*** INSTANCE VARIABLES ***/
    // size of build slots
    internal float buildSlotSize = 1f;
    // name and representation of piece being created
    internal PieceInfo pieceBeingMade;





    /*** INSTANCE PROPERTIES ***/
    // virtual board used in the creation of this piece 
    internal VirtualBoard<PieceBuildingSlot> VirtualBoardUsed { get; set; }
}
