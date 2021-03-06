﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// type alias, (# of players, # of rows, # of cols, piece resolution, relative gap size)
using DimensionsData = System.Tuple<byte, byte, byte, byte, float>;


// script which controls the behaviour of the game creation process 
public class GameCreationHandler : ProcessHandler<GameCreationHandler>
{
    /*** INSTANCE VARIABLES ***/
    // dimension data - stored for other handlers
    internal byte numOfPlayers;
    internal byte numOfRows;
    internal byte numOfCols;
    internal float gapSize;

    // board to be used in the game
    internal BoardInfo boardAtStart;

    // pieces to be used in the game, each piece is tied to its index in this list
    internal List<PieceInfo> pieces;

    // resolution of the pieces
    internal byte pieceResolution;

    // rules of the game, sorted according to player's turn and trigger piece 
    //   with noSquare denoting panel rules: rules[(player, trigPiece)] = info
    internal Dictionary<byte, Dictionary<byte, List<RuleInfo>>> rules;

    // player who plays at the start of the game
    internal byte startingPlayer;

    // the winning conditions of the game
    //   in pairs of (structure, winner) where if structure is found in the board
    //   somewhere, then winner will win the game
    internal List<WinCondInfo> winConditions;





    /*** INSTANCE PROPERTIES ***/
    internal byte NumOfRows 
    {
        get => boardAtStart.NumOfRows;
    }

    internal byte NumOfCols
    {
        get => boardAtStart.NumOfCols;
    }

    internal float GapSize
    { 
        get => boardAtStart.GapSize;
    }

    internal float SquareSize
    {
        get => BoardCreationHandler.GetHandler().BoardSquareSize;
    }






    /*** INSTANCE METHODS ***/
    // list of default player names, where player # n, index n-1, is 'Player n'
    internal List<string> DefaultPlayerNames() 
    {
        List<string> names = new List<string>();
        for (int i = 0; i < numOfPlayers; i++) 
        {
            names.Add("Player " + (i+1));
        }

        return names;
    }



    // finishes game creation
    internal GameInfo FinalizeGame(string gameName) 
    {
        // TODO Allow custom player names in future versions 

        // creates game info
        GameInfo gameMade = new GameInfo(boardAtStart, pieces, pieceResolution,
                                         numOfPlayers, startingPlayer, 
                                         DefaultPlayerNames(),
                                         rules, winConditions);


        // serializes it to file with name of game in games folder
        BinaryFormatter binFormat = new BinaryFormatter();
        FileStream gameFile = File.Create(
            ProgramData.gamesFolderPath + 
            Path.DirectorySeparatorChar + 
            gameName.Replace(' ', '_') + ".gam");
        binFormat.Serialize(gameFile, gameMade);
        gameFile.Close();

        return gameMade;
    }



    // creates a "PosInfo[,][,] array-like obj" which updates when source is updated
    internal Linked2D<byte, PosInfo[,]> LinkVisRepTo(byte[,] source)
    {
        return new Linked2D<byte, PosInfo[,]>
            (
                source,
                (i) =>
                {
                    if (i == PieceInfo.noPiece || i == PieceInfo.noSquare)
                    {
                        return PosInfo.NothingMatrix(pieceResolution, pieceResolution);
                    }
                    else
                    {
                        return pieces[i].visualRepresentation;
                    }
                }
            );
    }



    // resets variables used for creation of game
    internal void StartNewGame(DimensionsData data) 
    {
        // unpacks and assigns data
        (numOfPlayers, numOfRows, numOfCols, pieceResolution, gapSize) = data;

        boardAtStart = BoardInfo.DefaultBoard
            (
                numOfRows, numOfCols,
                BoardCreationHandler.GetHandler().BoardSquareSize, gapSize,
                BoardCreationHandler.defaultBoardColour
            );

        pieces = new List<PieceInfo>();

        rules = new Dictionary<byte, Dictionary<byte, List<RuleInfo>>>();
        for (byte plyr = 0; plyr < numOfPlayers; plyr++) 
        {
            rules[plyr] = new Dictionary<byte, List<RuleInfo>>();
        }
        
        winConditions = new List<WinCondInfo>();
    }
}
