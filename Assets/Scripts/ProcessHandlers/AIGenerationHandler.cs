using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerationHandler : ProcessHandler<AIGenerationHandler>
{
    /*** STATIC VARIABLES ***/
    // default waiting text while AI is being generated
    internal static string defaultWaitText = 
        "Please Wait \u0014 Generating AI . . . ";

    internal static string doneWaitText =
        "Finished! Thanks for waiting!";




    /*** INSTANCE VARIABLES ***/
    List<BotInfo> botsBeingGenerated;





    /*** INSTANCE METHODS ***/
    internal void GenerateBots(Game game) 
    {
        // clears old bots stored
        botsBeingGenerated = new List<BotInfo>();

        // see if an AI has already been created
        string botsPath = ProgramData.botsFolderPath +
            + Path.DirectorySeparatorChar + game.Info.name;
        BinaryFormatter bf = new BinaryFormatter();
        if (Directory.Exists(botsPath) && File.Exists(botsPath + "PLAYER_0")) // retrieves them if so
        {
            foreach (string path in Directory.EnumerateFiles(botsPath))
            {
                FileStream botFile = File.Open(path, FileMode.Open);
                botsBeingGenerated.Add((BotInfo)bf.Deserialize(botFile));
                botFile.Close();
            }

        }
        else // generates new AIs if not
        {
            Directory.CreateDirectory(botsPath);
            botsBeingGenerated = AIGenerator.Generate(game.Info);

            // serializes bots made
            Directory.CreateDirectory(botsPath);
            for (int p = 0; p < game.Info.numOfPlayers; p++) 
            {
                string botFilePath = botsPath + Path.DirectorySeparatorChar +
                    "PLAYER_" + p;
                FileStream botFile = File.Create(botFilePath);
                bf.Serialize(botFile, botsBeingGenerated[p]);
            }
        }

        TransitionHandler.GetHandler().RequestTransition
                (
                    GenerateAI.GetProcess(),
                    SetupAIs.GetProcess()
                );

    }



    internal List<BotInfo> RetrieveBots() 
    {
        return botsBeingGenerated;
    }





}
