using System;
using System.Collections.Generic;


// AI files name format: (game name)-(player #).bot
// data about AI bots 
[Serializable]
public class BotInfo
{
    /*** INNER CLASSES ***/
    // represents how the AI decides how to play
    public enum AIMode 
    { 
        RandomPlay, 
        BackPropagating
    }





    /*** INSTANCE VARIABLES ***/
    // the turn this AI plays
    public readonly byte turnPlays;

    // game this bot belongs to
    public readonly GameInfo game;

    // how the AI chooses a move
    public readonly AIMode mode;





    /*** CONSTRUCTORS ***/
    // generates a bot that plays randomly
    internal BotInfo(GameInfo gameInfo, byte turn) 
    {
        game = gameInfo;
        turnPlays = turn;
        mode = AIMode.RandomPlay;
    }





    /*** INSTANCE METHODS ***/
    // calculates what the bot believes is the 'best' move to play
    internal Tuple<RuleInfo, byte, byte> ChooseMove(Game game) 
    {
        // looks at all possible moves at current game start
        List<Tuple<RuleInfo, byte, byte>> moves = game.AllMovesPossible();

        // AI cannot play if there is no move to play 
        if (moves.Count == 0) 
        {
            return null;
        }

        switch (mode) 
        {
            case AIMode.RandomPlay:
                Random rng = new Random();
                return moves[rng.Next(0, moves.Count)];
        }

        // reaching here should be impossible
        throw new System.Exception("Invalid AI mode");
    }





}
