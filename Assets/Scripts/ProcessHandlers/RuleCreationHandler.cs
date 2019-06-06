using UnityEngine;
using System.Collections.Generic;

// script which controls the process of rule creation
public class RuleCreationHandler
{
    /*** STATIC VARIABLES ***/
    // default colour of the piece button currently selected
    private static Color selectedPieceColour =
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);



    /*** INSTANCE VARIABLES ***/
    // information on board being created
    private RuleInfo ruleBeingMade;

    // true iff. selecting a piece to be the 'trigger' piece
    //   (piece which 'plays' the rule when clicked)
    private bool selectingTriggerPiece;

    // true iff. making the state of the area after application of the rule
    private bool settingBoardAfter; 

    // information on the piece currently selected to be placed 
    //  (or if 'no piece' selected)
    private byte pieceSelected;

    // the piece spawning slots used in describing the area affected
    private List<PieceSpawningSlot>[,] spawningSlots;


    /*** INSTANCE METHDOS ***/
    // remove all values used for naking another rule 
    // NOTE: values assigned to null, 
    //       the method does not actually destroy old spawning slots used
    public void ClearOldValues() 
    {
        ruleBeingMade = null;
        selectingTriggerPiece = false;
        settingBoardAfter = false;
        pieceSelected = 255;
        spawningSlots = null;
    }
    
}
