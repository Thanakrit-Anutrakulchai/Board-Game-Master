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
        RandomSmart1, // random unless if can win or not win in one move 
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
    // generates a bot that plays randomly except to win or not lose on next turn
    internal BotInfo(GameInfo gameInfo, byte turn) 
    {
        game = gameInfo;
        turnPlays = turn;
        mode = AIMode.RandomSmart1;
    }





    /*** INSTANCE METHODS ***/
    // calculates what the bot believes is the 'best' move to play
    internal Tuple<RuleInfo, byte, byte> ChooseMove(Game curState) 
    {
        // looks at all possible moves at current game start
        List<Tuple<RuleInfo, byte, byte>> moves = curState.AllMovesPossible();

        // AI cannot play if there is no move to play 
        if (moves.Count == 0) 
        {
            return null;
        }

        Random rng = new Random();
        switch (mode) 
        {
            case AIMode.RandomPlay:
                return moves[rng.Next(0, moves.Count)];
            case AIMode.RandomSmart1:
                var winningMoves = moves.FindAll
                    (
                        (trip) =>
                        {
                            Game nextState = 
                                trip.Item1.Apply(curState, trip.Item2, trip.Item3)[0];

                            // check if game has been won
                            foreach (WinCondInfo winCond in nextState.Info.winConditions)
                            {
                                if (winCond.Check(nextState, out byte winner))
                                {
                                    // play move that allows victory 
                                    if (winner == turnPlays) 
                                    {
                                        return true;
                                    }
                                }
                            }

                            return false;
                        }
                    );
                if (winningMoves != null && winningMoves.Count > 0) 
                {
                    // return random winning move
                    return winningMoves[rng.Next(0, winningMoves.Count)];
                }
                else 
                {
                    var notLosingMoves = moves.FindAll
                        (
                            (trip) => 
                            {
                                Game nextState =
                                trip.Item1.Apply(curState, trip.Item2, trip.Item3)[0];

                                // check if game has not been won by someone else
                                foreach (WinCondInfo winCond in nextState.Info.winConditions)
                                {
                                    if (winCond.Check(nextState, out byte winner))
                                    {
                                        // play move that allows victory 
                                        if (winner != turnPlays)
                                        {
                                            return false;
                                        }
                                    }
                                }
                                return true;
                            }
                        );

                    if (notLosingMoves != null && notLosingMoves.Count > 0) 
                    {
                        // return random not-losing move
                        return notLosingMoves[rng.Next(0, notLosingMoves.Count)];
                    }
                    else 
                    {
                        // returns random move
                        return moves[rng.Next(0, moves.Count)];
                    }
                }
        }

        // reaching here should be impossible
        throw new System.Exception("Invalid AI mode");
    }





}
