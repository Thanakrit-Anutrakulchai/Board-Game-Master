using System;
using System.Collections.Generic;


// class representing a custom game and its behaviours 
//  (that is being played or modified)
public class Game
{
    /*** INSTANCE VARIABLES ***/
    // the current player during this turn 
    internal byte currentPlayer;

    // the current state of the board 
    internal BoardInfo boardState;

    // contains key-value pairs of turns played by bots, and the respective bot
    internal Dictionary<byte, BotInfo> bots;





    /*** INSTANCE PROPERTIES ***/
    // information about (setting up) the game
    public GameInfo Info { get; }




    /*** CONSTRUCTORS ***/
    // instantiates with given game info, board state, and current player
    internal Game(GameInfo gInfo, BoardInfo bInfo, byte curPlayer) 
    {
        Info = gInfo;
        boardState = bInfo;
        currentPlayer = curPlayer;
    }



    // instantiates with given game info in the board state at start of game
    //  the syntax here is just C# constructor chaining
    internal Game(GameInfo gInfo) : 
        this(gInfo, gInfo.boardAtStart, gInfo.startingPlayer) { }





    /*** INSTANCE METHODS ***/
    // lists of all moves currently usable at the current game state
    //   tuples returned are in form: (subrule, rowPos clicked, colPos clicked)
    internal List<Tuple<RuleInfo, byte, byte>> AllMovesPossible() 
    {
        byte[,] brdStateRep = boardState.BoardStateRepresentation;
        List<Tuple<RuleInfo, byte, byte>> accu = new List<Tuple<RuleInfo, byte, byte>>();
        for (byte r = 0; r < brdStateRep.GetLength(0); r++) 
        { 
            for (byte c = 0; c < brdStateRep.GetLength(1); c++) 
            {
                List<RuleInfo> rules = Info.rules[currentPlayer][brdStateRep[r, c]];
                foreach (RuleInfo subrule in rules) 
                {
                    if (subrule.Apply(this, r, c).Count > 0)
                    {
                        accu.Add(Tuple.Create(subrule, (byte)r, (byte)c));
                    }
                }
            }
        } // end foreach in double for loop appending all usable rules to list

        return accu;
    }




    // TODO TEMP: turn this into full-blown evaluation function for AI?
    // true iff. this and the otherGame has same board state and current player
    //   note that the colouring of the boards are ignored
    internal bool SameStateAs(Game otherGame)
    {
        // ensure board states are the same
        // first, ensure sizes are the same
        if ((boardState.NumOfRows != otherGame.boardState.NumOfRows) ||
            (boardState.NumOfCols != otherGame.boardState.NumOfCols))
        {
            return false; // otherwise, they clearly are not the same
        }

        // check board states equality, square/piece-wise 
        for (byte r = 0; r < boardState.NumOfRows; r++) 
        { 
            for (byte c = 0; c < boardState.NumOfCols; c++) 
            { 
                if (boardState.BoardStateRepresentation[r, c] != 
                    otherGame.boardState.BoardStateRepresentation[r, c]) 
                {
                    return false; // false if a single piece/square mismatched
                }
            }
        }


        // if boards are same, true iff. current players are the same, also
        return (currentPlayer == otherGame.currentPlayer);
    }
}
