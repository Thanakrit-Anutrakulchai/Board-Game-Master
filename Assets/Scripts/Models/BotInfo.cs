using System;
using System.Collections.Generic;


// AI files name format: (game name)-(player #).bot
// data about AI bots 
public class BotInfo
{
    /*** INNER CLASSES ***/
    // represents how the AI decides how to play
    public enum AIMode 
    { 
        RandomPlay
    }





    /*** INSTANCE VARIABLES ***/
    public AIMode mode;





    /*** INSTANCE METHODS ***/
    // calculates what the bot believes is the 'best' move to play
    internal Tuple<RuleInfo, byte, byte> ChooseMove(Game game) 
    { 
        switch (mode) 
        {
            case AIMode.RandomPlay:
                List<Tuple<RuleInfo, byte, byte>> moves = game.AllMovesPossible();
                
                Random rng = new Random();
                return moves[rng.Next(0, moves.Count)];
        }

        // reaching here should be impossible
        throw new System.Exception("Invalid AI mode");
    }





}
