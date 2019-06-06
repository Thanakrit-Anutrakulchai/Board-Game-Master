using UnityEngine;

// script which controls the behaviour of the piece customization panel 
//  in the piece creation process 
public class PieceCreationHandler
{
    /*** INSTANCE VARIABLES ***/
    // a template for spawning piece building slots
    internal GameObject pieceBuildingSlot;
    // size of build slots
    internal float buildSlotSize = 1;
    // name and representation of piece being created
    internal PieceInfo pieceInfo;
}
