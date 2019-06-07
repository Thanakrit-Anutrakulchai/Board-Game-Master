using UnityEngine;

public abstract class PieceSlot : MonoBehaviour
{
    // fixed variables determining size and position of cube spawned
    protected static Vector3 spawnOffset = new Vector3(0, 0, 0);
    // default cube size: 1x1x1, default plane size:10x10
    protected static int relScale = 10;



    /***  INSTANCE VARIABLES ***/
    // co-ordinates corresponding to vis.rep. indexes this object 
    //  is associated with, i.e. position inside a board's square
    internal byte pieceRow;
    internal byte pieceCol;

    // co-ordinates of the square of the board this smaller slot is in
    internal byte boardRow;
    internal byte boardCol;



    /*** CONSTRUCTORS ***/
    internal PieceSlot() { }
    internal PieceSlot(byte pr, byte pc, byte br, byte bc) 
    {
        pieceRow = pr;
        pieceCol = pc;
        boardRow = br;
        boardCol = bc;
    }





    /*** INSTANCE METHODS ***/
    // method to be called upon creation of PieceSlot game object
    internal abstract void OnCreate();
}
