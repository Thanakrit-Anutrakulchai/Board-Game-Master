using UnityEngine;

// Behaviour for slots used for making custom game pieces
internal class PieceBuildingSlot : PieceSlot
{
    /*** INSTANCE VARIABLES ***/
    // link to panel, which has array representation of piece being built
    internal PieceCreationHandler associatedPanel;

    // cube which makes up pieces, in build mode 
    internal PieceCubeBuildMode pieceCubeBuildMode;

    /*** METHODS ***/
    // spawns cube and update visual rep. array when clicked
    private void OnMouseDown()
    {
        // scale and position cube based on plane's scale and position
        pieceCubeBuildMode.transform.localScale = 
            this.transform.localScale * relScale;
        Vector3 posToSpawn = this.transform.position + spawnOffset;
        posToSpawn.y += pieceCubeBuildMode.transform.localScale.y / 2;


        // creates the piece cube and instantiates its variables
        GameObject cubeMade 
            = Instantiate(pieceCubeBuildMode.gameObject, posToSpawn, Quaternion.identity);
        PieceCubeBuildMode cubeMadeScript = 
            cubeMade.GetComponent<PieceCubeBuildMode>();
        cubeMadeScript.rowPos = this.rowPos;
        cubeMadeScript.colPos = this.colPos;
        cubeMadeScript.AssociatedHandler
            = this.associatedPanel;

        // will delete cube after creation of piece
        Utility.objsToDelete.Add(cubeMade);

        // adds information about the piece to the rep. array
        // TODO
        //  TEMP: stores colour info as (0 0 0) for now
        this.associatedPanel.pieceInfo.visualRepresentation[rowPos, colPos]
            = new PosInfo.RGBData(0, 0, 0);
    }


}
