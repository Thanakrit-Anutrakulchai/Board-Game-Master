

// class which controls behaviour of PieceCube game objects
//  while in piece building process
public class PieceCubeBuildMode : PieceCube
{

    /*** INSTANCE VARIABLES ***/
    // 2D array representing how the piece is constructed
    internal PieceCreationHandler associatedHandler;



    /*** METHODS ***/
    private void OnCreate() { }

    // delete object when clicked, both from representation and from game world
    private void OnMouseDown()
    {
        // removes piece from representation array
        associatedHandler.pieceInfo.visualRepresentation[rowPos, colPos]
            = new PosInfo.Nothing();

        // deactivates object in game world
        Destroy(this.gameObject);
    }
}
