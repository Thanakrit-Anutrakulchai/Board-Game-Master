﻿using System;
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
    private Text congratulatoryText;

    // the game currently being played 
    internal Game gameBeingPlayed;






    /*** INSTANCE PROPERTIES ***/
    // virtual board encoding data about game board
    VirtualBoard<PieceSpawningSlot> VirtualBoardUsed { get; set; }





    /*** INSTANCE METHODS ***/
    // starts the process of playing a custom game 
    internal void StartGame()
    {
        // TODO
    }


    // starts the game
    internal void Play() 
    {
        // TODO

        // retrievs game info, calculate where to start tiling
        GameInfo gmInf = gameBeingPlayed.info;
        BoardInfo startBoard = gmInf.boardAtStart;
        Vector3 start = new Vector3(-startBoard.Width / 2, 10, -startBoard.Height / 2);

        SetupHandler bh = 
            this.gameObject.GetComponent<SetupHandler>();

        float spawnSlotSize = (bh.BoardSquareSize/10) / gmInf.pieceResolution;

        // tiles and assigns appropriate variables to piece spawning slots
        Utility.TileAct(start, bh.pieceSpawningSlot, spawnSlotSize,
            gmInf.numOfRows, gmInf.numOfCols, gmInf.pieceResolution,
            gmInf.boardAtStart.sizeOfGap,
            (slot, boardR, boardC, pieceR, pieceC) =>
            {
                // assigns variables
                PieceSpawningSlot spawnSlotScr =
                    slot.GetComponent<PieceSpawningSlot>();
                spawnSlotScr.game = gameBeingPlayed;
                spawnSlotScr.rowPos = pieceR;
                spawnSlotScr.colPos = pieceC;
                spawnSlotScr.boardRow = boardR;
                spawnSlotScr.boardCol = boardC;
                spawnSlotScr.Spawn();

                // adds object to list of item to destroy after creation process
                Utility.objsToDelete.Add(slot);
            });
    }



    // moves onto the next turn of the game
    public void NextTurn() 
    {
        //TODO 

        List<byte> winners = new List<byte>();
        // check if game has been won 
        foreach ((byte[,] state, byte winner) in gameBeingPlayed.Info.winConditions)
        {
            // player has won if there is a sub-structure of that type
            if (state.IsSubMatrixOf(gameBeingPlayed.boardState.BoardStateRepresentation)) 
            {
                winners.Add(winner);
            }
        }

        // TODO ask whether multiple winners is a TIE or all wins or what
        GameEnded(winners);
    }



    // announces that game has been won and ends the game
    private void GameEnded(List<byte> winners) 
    {
        // TODO 
        // VERY TEMP.
        Debug.Log("List of Winners:");
        foreach (byte winner in winners) 
        {
            Debug.Log("\tPlayer No." + winner);
        }


        // TODO add custom text
        // declare that no one has won... if no one has won
        if (winners.Count == 0) 
        {
            congratulatoryText.text = 
                "Oh no! No one has won!";
        } 
        else if (winners.Count == 1) // if there is 1 clear winner, annouce it
        {
            congratulatoryText.text = 
                "The game has ended! The winner is: Player No." + winners[0];
        }
        else // list all of the winners 
        {
            // TODO 
            congratulatoryText.text = 
                "The game has ended! Multiple people won!";
        }
    }
}