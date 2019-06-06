using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This script manages custom games in play mode, 
//  deals with what happens when user interacts with pieces and boards 
public class GamePlayHandler : MonoBehaviour
{
    /*** STATIC VARIABLES ***/
    // the position of the camera at the start of a custom game
    private static Vector3 cameraStartPosition =
        new Vector3(0, 100, 0);

    // a link to the current program state 
    private static ProgramData programState =
        Camera.main.GetComponent<ProgramData>();




    /*** INSTANCE VARIABLES ***/
    // congratulatory text for the winner(s) 
    public Text congratulatoryText;

    // the game currently being played 
    public Game gameBeingPlayed;


    /*** INSTANCE METHODS ***/
    // starts the process of playing a custom game 
    public void ProcessStart()
    {
        // centers camera 100 units above origin
        Camera.main.transform.position = cameraStartPosition;

        // updates state
        programState.CurrentState = ProgramData.State.ChoosingGame;
        
        // switch canvas to the 'choose a game' canvas
        introCanvas.gameObject.SetActive(false);
        chooseGameCanvas.gameObject.SetActive(true);


        // TODO replace this with unified method

        // ensures games folder exists
        //  will not create/overwrite if folder already exists
        Directory.CreateDirectory(gamesFolderPath);

        // clear previous list of games 
        foreach (Button b in chooseGameScrView.content.GetComponentsInChildren<Button>())
        {
            if (!b.Equals(gameButtonTemplate))
            {
                Destroy(b);
            }
        }

        // populates list of playable games with clickable buttons
        IEnumerable<string> gameNames = Directory.EnumerateFiles(gamesFolderPath, "*.gam");
        foreach (string gmPath in gameNames)
        {
            // recover name from path
            int nameStart = gmPath.LastIndexOf('/') + 1;
            int nameEnd =
                gmPath.IndexOf(".gam", System.StringComparison.Ordinal);
            string gmName = gmPath.Substring(nameStart, nameEnd - nameStart);
            // recover display name (player inputted) by putting spaces back in
            gmName = gmName.Replace('_', ' ');

            // puts a button with the game's name under the displayed scroll view
            // switch canvas and starts game when button is clicked
            Button gameButton =
                Utility.CreateButton(gameButtonTemplate, chooseGameScrView.content, gmName,
                delegate
                {
                    // switch canvas 
                    chooseGameCanvas.gameObject.SetActive(false);
                    playGameCanvas.gameObject.SetActive(true);

                    // retrieve game information from games folder
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream gameFile = File.Open(gmPath, FileMode.Open);
                    GameInfo gmInfo = (GameInfo)(bf.Deserialize(gameFile));
                    gameFile.Close();


                    // assign and start the game
                    gameHandler.gameBeingPlayed = new Game(gmInfo);
                    gameHandler.Play();
                });


        }




        // TODO
    }


    // starts the game
    public void Play() 
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
