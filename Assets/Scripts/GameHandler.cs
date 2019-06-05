using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This script manages custom games in play mode, 
//  deals with what happens when user interacts with pieces and boards 
public class GameHandler : MonoBehaviour
{
    /*** INSTANCE VARIABLES ***/
    // congratulatory text for the winner(s) 
    public Text congratulatoryText;

    // the game currently being played 
    public Game gameBeingPlayed;


    /*** INSTANCE METHODS ***/
    // starts the game
    public void Play() 
    {
        // TODO

        // retrievs game info, calculate where to start tiling
        GameInfo gmInf = gameBeingPlayed.info;
        BoardInfo startBoard = gmInf.boardAtStart;
        Vector3 start = new Vector3(-startBoard.width / 2, 10, -startBoard.height / 2);

        ButtonHandler bh = 
            this.gameObject.GetComponent<ButtonHandler>();

        float spawnSlotSize = bh.boardSquareSize / gmInf.pieceResolution;

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

        // check if game has been won 
        foreach ((Game state, byte winner) in gameBeingPlayed.info.absoluteWinConditions)
        { 
            if (gameBeingPlayed.SameStateAs(state)) 
            {
                // TODO
                GameEnded(new List<byte>(new byte[] { winner }));
                return;
            }
        }
    }



    // announces that game has been won and ends the game
    public void GameEnded(List<byte> winners) 
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
