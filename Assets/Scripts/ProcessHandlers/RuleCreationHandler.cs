using UnityEngine;
using System.Collections.Generic;

// script which controls the process of rule creation
public class RuleCreationHandler : ProcessHandler<RuleCreationHandler>
{
    /*** STATIC VARIABLES ***/
    // default colour of the piece button currently selected
    private static Color selectedPieceColour =
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);



    /*** INSTANCE VARIABLES ***/
    // true iff. this is a rule which triggers on piece/board click
    //   otherwise, it is a rule activated only from the panel
    private bool makingTriggerRule;

    // true iff. selecting a piece to be the 'trigger' piece
    //   (piece which 'plays' the rule when clicked)
    private bool selectingTriggerPiece;

    // true iff. making the state of the area after application of the rule
    //   false iff. giving the state before application
    private bool settingBoardAfter; 

    // information on the piece currently selected to be placed 
    //  (or if 'no piece' selected)
    private byte pieceSelected;

    // the 'relative changes' of the area affected
    private RuleInfo.SquareChange[,] relChangesBeingMade; 

    // rule can only be activated on this player's turn (represented with a byte)
    private byte usableOn;

    // the player playing the next turn
    private byte nextPlayer;

    // index and location of the trigger piece in relChange
    //  the trigger piece is the one which activates the rule when clicked
    //  PieceInfo.noPiece for a rule that activates upon clicking an empty square
    //  PieceInfo.noSquare for panel rules (activated from scroll view during play)
    private byte triggerPiece;
    private byte triggerRow;
    private byte triggerCol;

    // representation of area of board affected before application of rule
    private List<byte>[,] areaBefore;

    // representation of area of board affected after application of rule
    private byte[,] areaAfter;






    /*** INSTANCE METHDOS ***/
    // finalizes rule and creation process and returns rule created
    internal RuleInfo FinalizeRule(string ruleName) 
    {
        // TODO
        throw new System.NotImplementedException();
    }

    // remove all values used for naking another rule 
    // NOTE: values assigned to null, 
    //       the method does not actually destroy old spawning slots used
    internal void StartNewRule() 
    { 
    
    }
    
}
