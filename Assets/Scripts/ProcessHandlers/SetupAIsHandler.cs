using System.Collections.Generic;
using UnityEngine;

// class which controls setting up AIs 
public class SetupAIsHandler : ProcessHandler<SetupAIsHandler>
{
    /*** STATIC VARIABLES ***/
    // default colours of selected player buttons
    internal static Color selectedPlayerColour =
        new Color(36 / 255f, 185 / 255f, 46 / 255f, 1);





    /*** INSTANCE VARIABLES ***/
    // bots that will play on their corresponding turn during the game
    internal Dictionary<byte, BotInfo> botsSetup;





    /*** INSTANCE METHODS ***/
    internal void NewBotsSetup()
    {
        botsSetup = new Dictionary<byte, BotInfo>();
    }



    internal Dictionary<byte, BotInfo> FinalizeBotsSetup() 
    {
        return botsSetup;
    }
}
