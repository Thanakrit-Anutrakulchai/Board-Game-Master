﻿using System;
using System.Collections.Generic;

// class representing a 'rule' of a custom game
//   e.g. Move Piece in Checkers for moving a piece diagonally
[System.Serializable]
public class RuleInfo
{
    /*** INNER CLASSES ***/
    // whether a piece in a certain square is changed by the rule or not
    [System.Serializable]
    public class SquareChange 
    { 
        private SquareChange() { } // hides default constructor

        [System.Serializable]
        public class Unaffected : SquareChange { }
        [System.Serializable]
        public class Changed : SquareChange 
        {
            // a list of every single piece that the rule applies to
            public List<byte> pieceChangedFrom;

            // The piece to be replaced (or to replace the old piece)
            // NOTE: This program currently only supports one piece here
            public byte pieceChangedTo;
        }
    }

    /*** INSTANCE VARIABLES ***/
    // the name of this rule 
    public string name;

    // the 'relative changes' of the area affected
    public SquareChange[,] relChanges;

    // index and location of the trigger piece in relChange
    //  the trigger piece is the one which activates the rule when clicked
    public byte triggerPiece;
    public byte triggerRow;
    public byte triggerCol;  




    /*** INSTANCE METHODS ***/
    // for using this rule to affect the game 
    //   takes current state of game, and location where trigger piece is
    //   returns all possible game states reachable from "playing" this rule
    //   If the rule cannot be applied, the list is empty
    public List<Game> Apply(Game gm, byte tRow, byte tCol) 
    {
        // get position of bottom left corner of area changed
        //  checks whether they are both still within range of being a byte
        bool originStillBytes = tRow.SubCheck(triggerRow, out byte originRow);
        originStillBytes &= tCol.SubCheck(triggerCol, out byte originCol); 

        // guard against invalid (not in byte range) positions 
        if (!originStillBytes) 
        {
            // cannot successfully apply rule in this case -> no state possible
            return new List<Game>();
        }


        // ensures rule can be applied by checking all affected pieces
        for (byte rRel = 0; rRel < relChanges.GetLength(0); rRel++) 
        { 
            for (byte cRel = 0; cRel < relChanges.GetLength(1); cRel++) 
            {

                // gets absolute position in board
                //  and ensures they are still bytes
                bool absPosStillBytes = rRel.AddCheck(originRow, out byte rAbs);
                absPosStillBytes &= cRel.AddCheck(originCol, out byte cAbs);

                // blocks against invalid (not within byte range) positions
                if (!absPosStillBytes) 
                {
                    // cannot successfully apply rule -> no state possible
                    return new List<Game>();
                }

                // tries and recover the piece at that position
                bool gotPce = gm.boardState.TryGetPiece(rAbs, cAbs, out byte pce);
                if (gotPce) // if it exists
                {
                    SquareChange change = relChanges[rRel, cRel];
                    if (change is SquareChange.Changed) 
                    {
                        SquareChange.Changed sqChng =
                            change as SquareChange.Changed;

                        if (!sqChng.pieceChangedFrom.Contains(pce)) 
                        {
                            // piece here is incompatible with rule
                            // -> rule cannot be applied -> no resulting state
                            return new List<Game>();
                        }
                    } // don't worry about pieces that are unaffected
                      //  even if they would normally be 'out of board'
                }
                else // if it is unaccessible i.e. out of board
                {
                    // cannot apply rule -> no resulting state 
                    return new List<Game>();
                }
            }
        } // end of for loop x2



        // if function has not yet returned, then rule is applicable

        // prepares new Game if rule may succesfully be applied
        Game resGame = new Game(gm.boardState.GetCopy(), gm.info.pieces);

        // loop through resGame's board, setting all pieces to after rule is applied
        //  2 loops are used in case copying the boardstate was unnecessary
        //  (in which case it is not copied and this loop is not run)
        for (byte rRel = 0; rRel < relChanges.GetLength(0); rRel++) 
        { 
            for (byte cRel = 0; cRel < relChanges.GetLength(1); cRel++) 
            {
                // TODO 
                // TEMP. 'redundant' chceck incase of unexpected error
                // this check had already been done, surely absPosStillBytes = true
                bool absPosStillBytes = rRel.AddCheck(originRow, out byte rAbs);
                absPosStillBytes &= cRel.AddCheck(originCol, out byte cAbs);
                if (!absPosStillBytes) 
                {
                    UnityEngine.Debug.Log
                        (
                            "Impossible error occured! " +
                            "Same calculation led to different results"
                        );
                }

                // assigns piece resulting from application of rule
                switch (relChanges[rRel, cRel]) 
                {
                    case SquareChange.Changed changed:
                        resGame.boardState.boardStateRepresentation[rAbs, cAbs] = 
                            changed.pieceChangedTo;
                        break;
                }

            }
        } // end of second for loop x2

        // return the list of possible game states 
        List<Game> resList = new List<Game>();
        resList.Add(resGame);
        return resList;
    }
}
