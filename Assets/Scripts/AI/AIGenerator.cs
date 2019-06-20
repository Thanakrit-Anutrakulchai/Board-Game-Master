using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


// class which handles the creation of custom AIs
public class AIGenerator : MonoBehaviour
{
    /*** STATIC METHODS ***/
    internal static List<BotInfo> Generate(GameInfo game)
    {
        List<BotInfo> bots = new List<BotInfo>();

        for (byte p = 0; p < game.numOfPlayers; p++) 
        {
            bots.Add(new BotInfo(game, p));
        }

        return bots;
    }





}
