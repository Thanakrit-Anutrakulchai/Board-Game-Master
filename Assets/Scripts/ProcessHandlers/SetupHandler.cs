using UnityEngine;
using System.IO;


// Class that handles what happens when the program starts
public class SetupHandler : ProcessHandler<SetupHandler>
{
    /*** AWAKE - CALLED BEFORE START ***/
    private void Awake()
    {
        // Application.persistentDataPath links to a folder which contains
        //  information about the gamess
        //  It can only be accessed in the Start or Awake method for MonoBehaviours
        ProgramData.gamesFolderPath = Application.persistentDataPath + 
            Path.DirectorySeparatorChar + "games";
        ProgramData.botsFolderPath = Application.persistentDataPath +
            Path.DirectorySeparatorChar + "bots";
    }






    /*** ACTIVATES AT START OF PROGRAM ***/
    // Start is called before the first frame update
    // assigns handler to every single button
    private void Start()
    {
        SetupStaticGenerics();

        // some setup is done in TransitionHandler, which has access
        //   to most (if not all) UI elements
        TransitionHandler th = Camera.main.GetComponent<TransitionHandler>();
        th.AddListenersToButtons();

        // creates games folder and bots folder if they do not exist yet
        Directory.CreateDirectory(ProgramData.gamesFolderPath);
        Directory.CreateDirectory(ProgramData.botsFolderPath);

        // generates and assigns string for checking names
        string checkStr = "";
        for (char c = 'A'; c <= 'Z'; c++)
        {
            checkStr += c;
        }

        for (char c = 'a'; c <= 'z'; c++)
        {
            checkStr += c;
        }

        for (int i = 0; i <= 9; i++)
        {
            checkStr += i;
        }

        checkStr += ' ';

        ProgramData.nameCheckString = checkStr;
    }






    /*** INSTANCE METHODS ***/
    // sets up static generic variables
    private void SetupStaticGenerics() 
    {
        Prefabs prefabs = Prefabs.GetPrefabs();
        PieceSlot<PieceBuildingSlot>.template = prefabs.pieceBuildingSlot;
        PieceSlot<PieceSpawningSlot>.template = prefabs.pieceSpawningSlot;
    }
}
