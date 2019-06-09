﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

// Items associated with the ChooseGame canvas
internal sealed class ChooseGame : Process<ChooseGame>, IAssociatedState<UnityEngine.Object, Game>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly ScrollRect chooseGameScrView;
    [SerializeField] internal readonly Button gameButtonTemplate;
    [SerializeField] internal readonly Button deleteAllGamesButton;
    [SerializeField] internal readonly Text warningText;

    // game to pass on to the Game Play process
    private Game gameToPass;

    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseGame;
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
            int indexNameEnd = path.IndexOf(".gam", StringComparison.Ordinal);
            int indexNameStart = path.LastIndexOf("/", StringComparison.Ordinal) + 1;
            int lengthOfName = indexNameEnd - indexNameStart;
            string gameName = path.Substring(indexNameStart, lengthOfName);

            // recovers original name by substituting spaces back in
            gameName = gameName.Replace('_', ' '); 

            // appends named button to the scroll view
            TransitionHandler transHandler = TransitionHandler.GetHandler();
            transHandler.CreateTransitionButton<UnityEngine.Object, Game, UnityEngine.Object>
                (
                    gameButtonTemplate,
                    chooseGameScrView.content,
                    gameName,
                    ChooseGame.GetProcess(),
                    PlayGame.GetProcess(),
                    delegate 
                    {
                        // retrieves information about game from file
                        FileStream file = File.Open(path, FileMode.Open);
                        BinaryFormatter binform = new BinaryFormatter();
                        GameInfo gameInfo = (GameInfo) binform.Deserialize(file);

                        // close file and prepare to pass info on
                        file.Close();
                        gameToPass = new Game(gameInfo);
                    }
                );
        }
    }
}