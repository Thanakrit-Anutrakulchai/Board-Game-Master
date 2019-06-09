using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;

// TODO
// REFACTOR code into smaller chunks

// Class that handles what happens when the program starts
public class SetupHandler : ProcessHandler<SetupHandler>
{
    /*** AWAKE - CALLED BEFORE START ***/
    private void Awake()
    {
        // Application.persistentDataPath links to a folder which contains
        //  information about the gamess
        //  It can only be accessed in the Start or Awake method for MonoBehaviours
        ProgramData.gamesFolderPath = Application.persistentDataPath + "/games";
    }





    /*** ACTIVATES AT START OF PROGRAM ***/
    // Start is called before the first frame update
    // assigns handler to every single button
    private void Start()
    {
        // some setup is done in TransitionHandler, which has access
        //   to most (if not all) UI elements
        TransitionHandler th = Camera.main.GetComponent<TransitionHandler>();
        th.AddListenersToButtons();

        // creates games folder if it does not exist yet
        Directory.CreateDirectory(ProgramData.gamesFolderPath);
    }






    /*** INSTANCE METHODS ***/

    // starts process of making a "relative" rule 
    //   which triggers when a piece or the board is clicked
    public void MakeRelRule() 
    {

        // TODO add checks
        // recover information about player's turn and size of area affected 
        bool parsedArea = Byte.TryParse(InputAreaAffected.text, out byte areaAffected);
        // do this -> bool parsedPlayer =

        // TODO do something if area not succesfully parsed
        if (!parsedArea) 
        { 
            // add code here
        }


        // generate rule, and area to be affected
        ruleCreationPanel.ruleBeingMade = new RuleInfo();
        ruleCreationPanel.ruleBeingMade.relChanges =
            RuleInfo.SquareChange.GetDefaultAreaAffected(areaAffected);



        // TODO generalize this code and the one in makeboard

        // clears all old buttons on the scroll view
        relRuleSelectPieceScrView.Clear(relRuleSelectPieceButtonTemplate);

        // populates the scroll view with buttons labeled with piece names
        for (byte index = 0; index < gameBeingMade.info.pieces.Count; index++)
        {
            // when clicked change colours of buttons
            //  and assigns piece associated to be current piece selected

            // index of the associated piece 
            //  index should not be used directly, as it *will* change
            //  after this iteration of the loop ends
            // indexAssocPiece is kind of like an upvalue in Lua
            byte indexAssocPiece = index;
            PieceInfo pce = gameBeingMade.info.pieces[index];

            Button pceButton =
                Utility.CreateButton(pieceButtonTemplate, relRuleSelectPieceScrView.content,
                pce.pieceName,
                (btn) => delegate
                {
                    // retrieve all buttons under the piece selection scrollview
                    Button[] buttons =
                        relRuleSelectPieceScrView.content.GetComponentsInChildren<Button>();
                    // changes colour of all piece selection buttons back
                    foreach (Button b in buttons)
                    {
                        b.GetComponent<Image>().color = Color.white;
                    }

                    // change colour of remove piece button
                    removePieceButton.GetComponent<Image>().color = Color.white;

                    // change colour of set trigger button
                    setTriggerPieceButton.GetComponent<Image>().color = Color.white;


                    // TODO 
                    // TEMP
                    // DEBUG
                    // index of piece selected
                    Debug.Log("INDEX OF PIECE SELECTED: " + indexAssocPiece);


                    // changes piece selected
                    ruleCreationPanel.pieceSelected = indexAssocPiece;

                    // changes this button's colour
                    btn.GetComponent<Image>().color =
                        RuleCreationHandler.selectedPieceColour;
                });
        }


        // tiles area to be affected 
        // calculates width and height of area 
        int numRows = ruleCreationPanel.ruleBeingMade.relChanges.GetLength(0);
        int numCols = ruleCreationPanel.ruleBeingMade.relChanges.GetLength(1);
        float height = numRows * BoardSquareSize + 
            (numRows - 1)*BoardSquareSize*gameBeingMade.boardState.sizeOfGap;
        float width = numCols * BoardSquareSize +
            (numCols - 1) * BoardSquareSize * gameBeingMade.boardState.sizeOfGap;

        //calculates start of tiling 
        Vector3 tilingStart = new Vector3(-width / 2, heightOfBoard, -height / 2);

        // tiles the area
        Utility.TileAct(tilingStart, pieceSpawningSlot,
                        gameBeingMade.info.spawnSlotSize,
                        (byte) numRows, (byte) numCols, 
                        gameBeingMade.info.pieceResolution,
                        gameBeingMade.info.boardAtStart.sizeOfGap,
                        (slot, boardR, boardC, pieceR, pieceC) =>
                        {
                            // assigns variables
                            PieceSpawningSlot spawnSlotScr =
                                slot.GetComponent<PieceSpawningSlot>();
                            spawnSlotScr.game = gameBeingMade;
                            spawnSlotScr.pieceRow = pieceR;
                            spawnSlotScr.pieceCol = pieceC;
                            spawnSlotScr.boardRow = boardR;
                            spawnSlotScr.boardCol = boardC;

                            // notes that this spawning slot is currently used
                            gameBeingMade.spawningsSlots[boardR, boardC].Add(spawnSlotScr);

                            // spawns cube at the corresponding position relative to the piece
                            spawnSlotScr.Spawn();

                            // adds object to list of item to destroy after creation process
                            Utility.objsToDelete.Add(slot);
                        });
    }


    // assigns the next piece clicked to be the 'trigger piece' 
    public void SetTriggerPiece() 
    {
        // TODO make this more clear to the user
        // button may not be used while setting the resulting state of the area
        if (ruleCreationPanel.settingBoardAfter) 
        {
            return;
        }

        // notes that in process of assigning trigger piece
        ruleCreationPanel.selectingTriggerPiece = true;

        // un-highlights all other buttons 
        Button[] buttons =
            relRuleSelectPieceScrView.GetComponentsInChildren<Button>();
        foreach (Button b in buttons) 
        {
            b.GetComponent<Image>().color = Color.white;
        }
        relRuleRemovePieceButton.GetComponent<Image>().color = Color.white;

        // highlights the select trigger piece button
        setTriggerPieceButton.GetComponent<Image>().color =
            RuleCreationHandler.selectedPieceColour;
    }


    // switches betweewn making a board before and after 
    public void RelRuleSwitchTo() 
    { 
        // TODO
    }


    // finishes process of making a custom, relative on-click rule 
    public void DoneRelRule() 
    {
        // adds relative rule made to the game 
        RuleInfo ruleMade = ruleCreationPanel.ruleBeingMade;
        byte triggerPiece = ruleMade.triggerPiece;
        gameBeingMade.info.rules[triggerPiece].Add(ruleMade);

        // update state 
        
    }


    // DELETE EVERY GAME STORED INSIDE OF THE GAMES FOLDER
    //  THAT IS WHERE ALL GAMES ARE LOCATED WHEN CREATED WITH THIS PROGRAM!
    public void DeleteAllGames() 
    {
        if (numTimesDeleteAllGamesClickedSinceDeletion > 0) 
        {
            // deletes all games, and resets the content of the scroll view
            Utility.DeleteAllSavedGames();

            // hide warning text
            areYouSureText.text = "";

            // reset num of times it has been clicked since last deletion
            numTimesDeleteAllGamesClickedSinceDeletion = 0;

            // change warning text back
            deleteAllGamesButton.GetComponentInChildren<Text>().text = 
                "DELETE ALL GAMES";

            // updates game state
            currentProgramState = ProgramData.Intro;

            // switches canvas back to main/intro canvas 
            //  (there's no game to choose to play anymore, why stay there?)
            chooseGameCanvas.gameObject.SetActive(false);
            introCanvas.gameObject.SetActive(true);
        } 
        else 
        {
            // asks, if has not asked already
            areYouSureText.text = "ARE YOU SURE?";
            deleteAllGamesButton.GetComponentInChildren<Text>().text = "YES!!!";

            numTimesDeleteAllGamesClickedSinceDeletion++; //increment count
        }
    }


    // appends information about game just created to a file which 
    //  stores information about all playable games
    public void DoneGame() 
    {
        // using .gam extension to stand for 'game' since there
        //  are no obvious conventions... 


        // Check if file (game with same name) already exists
        // check that name is proper (alphanum, non-empty)
        // TODO
        string gameName = enterGameNameInputField.text;
        enterGameNameInputField.text = ""; //reset text in input field

        // name file according to game, prepare to put in games folder
        // spaces are replaced with underscrolls for developpers' convenience
        //  shall be replaced back with spaces when displayed
        string gamePath = gamesFolderPath +
            "/" + (gameName.Replace(' ', '_')) + ".gam";



        // TODO
        // TEMP 
        // creates a file called TEMP_(id#) inside the games folder to 
        //  store all information about created game
        FileStream gameFile = File.Open(gamePath, FileMode.Create);

        // TEMP
        // serializes game data to file 
        //  temporarily tests serializing a '0'
        BinaryFormatter binFmt = new BinaryFormatter();
        binFmt.Serialize(gameFile, gameBeingMade.info);


        // closes the file
        gameFile.Close();

        // updates state
        currentProgramState = ProgramData.Intro;

        // switches back to main screen
        makeGameCanvas.gameObject.SetActive(false);
        introCanvas.gameObject.SetActive(true);

    }
}
