using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

// Items associated with the ChooseGame canvas
internal sealed class ChooseGame : Process<ChooseGame>, 
    IAssociatedState<UnityEngine.Object, Game>
{
    /*** STATIC VARIABLES ***/
    private static Color selectedPieceColour = 
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);





    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal ScrollRect chooseGameScrView;
    [SerializeField] internal Button gameButtonTemplate;
    [SerializeField] internal Button generateAIButton;
    [SerializeField] internal Button playButton;
    [SerializeField] internal Button deleteAllGamesButton;
    [SerializeField] internal Text warningText;

    // game to pass on to the Game Play process
    private Game gameToPass;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseGame;
    }






    /*** START ***/
    // Add handlers to UI elements
    private void Start()
    {
        // makes button do what it says it will -- it really will!
        deleteAllGamesButton.onClick.AddListener
            (
                delegate
                {
                    Utility.DeleteAllSavedGames();
                    chooseGameScrView.Clear(gameButtonTemplate);
                }
            );

        // highlights chosen game button
        chooseGameScrView.WhenChosenChanges
            ((scrView) => delegate 
                { 
                    scrView.HighlightOnlyChosen(new List<Button>(), selectedPieceColour);
                }
            );
        
    }






    /*** INSTANCE METHODS ***/
    // Intro -> Choose Game
    public void OnEnterState(IAssociatedStateLeave<UnityEngine.Object> prevState, 
                                UnityEngine.Object args)
    {
        SetupUIs();
    }



    // Choose Game -> Play Game
    // passes game chosen
    public Game OnLeaveState(IAssociatedStateEnter<Game> nextState)
    {
        return gameToPass;
    }



    // populates scroll view with named buttons which starts a game when clicked
    private void SetupUIs()
    {
        // clears old buttons
        chooseGameScrView.Clear(gameButtonTemplate); 

        // creates directory if it does not exist yet
        Directory.CreateDirectory(ProgramData.gamesFolderPath);

        // gets name of all games
        string[] paths = Directory.GetFiles(ProgramData.gamesFolderPath);

        // populates scroll view
        foreach (string path in paths) 
        {
            // retrieves name of game
            int indexNameEnd = path.IndexOf(".gam", StringComparison.InvariantCulture);
            int indexNameStart = path.LastIndexOf("/", StringComparison.InvariantCulture) + 1;
            int lengthOfName = indexNameEnd - indexNameStart;
            string gameName = path.Substring(indexNameStart, lengthOfName);

            // recovers original name by substituting spaces back in
            gameName = gameName.Replace('_', ' ');

            // appends named button to the scroll view
            Utility.CreateButton
                (
                    gameButtonTemplate, 
                    chooseGameScrView.content,
                    gameName,
                    (btn) => delegate
                    {
                        // retrieves information about game from file
                        FileStream file = File.Open(path, FileMode.Open);

                        Debug.Log("DESERIALIZING AT: " + path);

                        BinaryFormatter binform = new BinaryFormatter();
                        GameInfo gameInfo = (GameInfo)binform.Deserialize(file);

                        // close file and prepare to pass info on
                        file.Close();
                        gameToPass = new Game(gameInfo);

                        chooseGameScrView.SetChosenItem(btn);
                    }
                );
                    
        } // finishes populating scroll view

    }
}
