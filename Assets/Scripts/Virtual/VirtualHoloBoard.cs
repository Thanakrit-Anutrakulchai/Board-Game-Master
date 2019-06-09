using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// HoloBoard for 'Hologram Board'
// Similar to Virtual Board, but provides ability to make non-opaque pieces,
//   and to "overlay" multiple pieces on top of each other on the same square
// Uses piece spawning slots
public class VirtualHoloBoard : MonoBehaviour
{
    /*** INSTANCE VARIABLES ***/
    /// <summary>
    /// The PieceSlots used to tile this board <para />
    /// Each list corresponds to slots used to tile each board square
    /// </summary>
    private readonly List<PieceSpawningSlot>[,] slots;

    // matrix of visual representations of what's on a board square
    internal readonly List<PosInfo[,]>[,] boardRepresentation;

    // other objects on the board (e.g. piece cubes)
    internal List<Object> otherObjsOnBoard;

    /// <summary>
    /// Handler which is called whenever a board square is clicked.
    /// The positions (index) of the square clicked is passed in, as well,
    /// starting from (r=0, c=0) on the bottom left and increasing up and rightwards
    /// </summary>
    private readonly UnityAction<VirtualHoloBoard, byte, byte> onClickHandler;
}
