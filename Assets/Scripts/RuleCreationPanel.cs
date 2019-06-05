using UnityEngine;

// script which controls the process of rule creation
public class RuleCreationPanel : MonoBehaviour
{
    /*** STATIC VARIABLES ***/
    // default colour of the piece button currently selected
    public static Color selectedPieceColour =
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);



    /*** INSTANCE VARIABLES ***/
    // information on board being created
    public RuleInfo ruleBeingMade;

    // true iff. selecting a piece to be the 'trigger' piece
    //   (piece which 'plays' the rule when clicked)
    public bool selectingTriggerPiece;

    // information on the piece currently selected to be placed 
    //  (or if 'no piece' selected)
    public byte pieceSelected;
}
