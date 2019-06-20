using UnityEngine;

// class for storing general information about the program
public class ProgramData
{
    /*** INNER CLASSES ***/
    internal enum State
    {
        ChooseBoardDim,
        ChooseGame,
        ChooseRuleArea,
        ChooseWinCondArea,
        GeneratingAI,
        Intro,
        MakeBoard,
        MakeGame,
        MakePiece,
        MakeRule,
        MakeWinCond,
        PaintBoard,
        PanelRule,
        PlayGame,
        RelativeRule,
        SettingAIs
    }





    /*** STATIC VARIABLES ***/
    // path to folder where all games made with/used in this program can be found
    //   (files with .gam extensions)
    internal static string gamesFolderPath; //assigned in SetupHandler

    // string for checking validity of a name
    //   constructured and assigned in setup handler
    internal static string nameCheckString;

    // path to where info about bots are stored
    internal static string botsFolderPath;

    // current state of the program
    internal static State currentState;
}
