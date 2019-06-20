using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This script manages custom games in play mode, 
//  deals with what happens when user interacts with pieces and boards 
public class GamePlayHandler : ProcessHandler<GamePlayHandler>
{
    /*** STATIC VARIABLES ***/
    // the position of the camera at the start of a custom game
    private static readonly Vector3 cameraStartPosition =
        new Vector3(0, 100, 0);
        




    /*** INSTANCE VARIABLES ***/
    // congratulatory text for the winner(s) 
    //private Text congratulatoryText;

    // the game currently being played 
    internal Game gameBeingPlayed;

    // true iff. game has ended (a player has won) 
    internal bool boardLocked;






    /*** INSTANCE PROPERTIES ***/
    // virtual board encoding data about game board
    internal VirtualBoard<PieceSpawningSlot> VirtualBoardUsed { get; set; }





    /*** INSTANCE METHODS ***/
    // starts the process of playing the custom game 
    internal void StartGame(Game game)
    {
        gameBeingPlayed = game;
        PlayGame playGame = PlayGame.GetProcess();

        Debug.Log("PIECE RES: " + game.Info.pieceResolution);


        // generates virtual board for the game
        Func<int, int, PosInfo[,]> getBrdState =
            (r, c) =>
            {
                byte pce = gameBeingPlayed.boardState.BoardStateRepresentation[r, c];
                byte pceRes = gameBeingPlayed.Info.pieceResolution;
                if (pce == PieceInfo.noPiece || pce == PieceInfo.noSquare) 
                {
                    return PosInfo.NothingMatrix(pceRes, pceRes);
                }

                return gameBeingPlayed.Info.pieces[pce].visualRepresentation;
            };

        Func<int, int, PosInfo> getBrdShape = 
            (r, c) => gameBeingPlayed.boardState.BoardShapeRepresentation[r, c];

        VirtualBoardUsed = new VirtualBoard<PieceSpawningSlot>
            ( 
                getBrdState.ToProvider(gameBeingPlayed.Info.NumOfRows, gameBeingPlayed.Info.NumOfCols), 
                getBrdShape.ToProvider(gameBeingPlayed.Info.NumOfRows, gameBeingPlayed.Info.NumOfCols),
                game.Info.pieceResolution, 
                game.Info.boardAtStart.SquareSize,
                game.Info.boardAtStart.GapSize,
                (brd, r, c) => 
                {
                    // lock board (do nothing) if game has ended 
                    if (boardLocked) 
                    {
                        return;
                    }

                    byte curPlayer = gameBeingPlayed.currentPlayer;
                    byte pceClicked =
                        gameBeingPlayed
                            .boardState
                            .BoardStateRepresentation[r, c];

                    List<RuleInfo> rules = 
                        gameBeingPlayed.Info
                                       .rules[curPlayer][pceClicked];

                    Debug.Log("NUMBER OF TRIGGERED RULES: " + rules.Count);

                    // clear old rules listed on panel
                    playGame.movesScrView.Clear(playGame.moveButtonTemplate);

                    foreach (RuleInfo rule in rules)    
                    {
                        List<Game> possibleGames = rule.Apply(gameBeingPlayed, r, c);

                        Debug.Log("APPLICATION RESULTS: " + possibleGames.Count + " POSSIBLE GAMES");
                        // dont display non-applicable rules
                        if (possibleGames.Count == 0) 
                        {
                            continue;
                        }

                        Utility.CreateButton
                            (
                                playGame.moveButtonTemplate,
                                playGame.movesScrView.content, 
                                rule.name, 
                                delegate 
                                {
                                    // TODO allow multiple in future versions ?
                                    // for now, only allow first state
                                    gameBeingPlayed = possibleGames[0];

                                    // refresh all squares
                                    VirtualBoardUsed.RefreshBoard();

                                    // move onto next term
                                    NextTurn(rule.nextPlayer);
                                }
                            );
                    }
                }
            );

        VirtualBoardUsed.SpawnBoard(SpatialConfigs.commonBoardOrigin);
        NextTurn(gameBeingPlayed.Info.startingPlayer);
    }



    // moves onto the next turn of the game
    public void NextTurn(byte player) 
    {
        // update current player
        gameBeingPlayed.currentPlayer = player;

        PlayGame playGame = PlayGame.GetProcess();
        // updates current player text
        playGame.curPlayerText.text = "CURRENT PLAYER: " + gameBeingPlayed.currentPlayer;

        // clear previous moves 
        playGame.movesScrView.Clear(playGame.moveButtonTemplate);

        // let bot choose a move on its turn
        if (gameBeingPlayed.bots.ContainsKey(player)) 
        {
            boardLocked = true;
            Tuple<RuleInfo, byte, byte> move = 
                gameBeingPlayed.bots[player].ChooseMove(gameBeingPlayed);
            // TODO change if multiple games per rule allowed in future versions
            gameBeingPlayed = move.Item1.Apply(gameBeingPlayed, move.Item2, move.Item3)[0];
        }


        // check if game has been won 
        List<byte> winners = new List<byte>();
        foreach (WinCondInfo winCond in gameBeingPlayed.Info.winConditions)
        {
            // player has won if there is a sub-structure of that type
            if ( winCond.Check(gameBeingPlayed, out byte winner) ) 
            {
                winners.Add(winner);
            }
        }

        if (winners.Count > 0)
        {
            GameEnded(winners);
        }
    }



    // announces that game has been won and ends the game
    // assumption that winners is non-empty
    private void GameEnded(List<byte> winners) 
    {
        boardLocked = true;

        // TODO Allow multiple winners per game in later versions
        PlayGame playGame = PlayGame.GetProcess();
        playGame.curPlayerText.text = "";
        playGame.winnerText.text =
            "Player " + winners[0] + " has won the game!";


        /* DEBUG CODE
        // VERY TEMP.
        Debug.Log("List of Winners:");
        foreach (byte winner in winners) 
        {
            Debug.Log("\tPlayer No." + winner);
        }


        // declare that no one has won... if no one has won
        if (winners.Count == 0) 
        {
            Debug.Log(
                "Oh no! No one has won!");
        } 
        else if (winners.Count == 1) // if there is 1 clear winner, annouce it
        {
            Debug.Log(
                "The game has ended! The winner is: Player No." + winners[0]);
        }
        else // list all of the winners 
        {
            Debug.Log(
                "The game has ended! Multiple people won!");
        }
        */
    }



    // method called when the quit game button is pressed
    internal void QuitGame() 
    {
        // destroys the game board
        VirtualBoardUsed.DestroyBoard();
    }





}