﻿using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;



// collection of useful variables and methods useful throughout the program
public class Utility : MonoBehaviour
{
    /*** STATIC VARIABLES ***/
    // games folder, where all files containing data about games are stored
    private static readonly string gamesFolderPath = 
        Application.persistentDataPath + "/games";

    // all the game objects to delete after a (creation/play) session
    public static List<GameObject> objsToDelete = new List<GameObject>();



    /*** STATIC METHODS ***/
    // clears and recreates games folder, REMOVES ALL SAVED GAMES!!!
    public static void DeleteAllSavedGames() 
    {
        // recreates directory if it is not there
        Directory.CreateDirectory(gamesFolderPath);
        // removes all files inside
        foreach (string path in Directory.GetFiles(gamesFolderPath)) 
        {
            File.Delete(path);
        }
    }


    // delete objects queued for deletion (all objects in objsToDelete)
    //  and clear the list for re-use
    public static void DeleteQueuedObjects() 
    {
        // delete each object
        foreach (GameObject obj in objsToDelete)
        {
            Destroy(obj);
        }
        objsToDelete.Clear(); // empty list for reuse
    }

    // this makes planes
    public static void Tile(Vector3 start, GameObject plane, float width, byte sqsXDir, byte sqsZDir,
                            byte sizeSmall)
    {
        TileAct(start, plane, width, sqsXDir, sqsZDir, sizeSmall, 0.5f,
            (_, _x, _z, _xSm, _zSm) => { });
    }


    // Utility function used to tile boards, while performing extraAct for each square made,
    //  passing in the object instantiated and the four loop variables
    //                                              (denoting which small/large squares it's in)
    // NOTE: width is the local scale of the smaller squares
    //  gap between large squares = 
    public static void TileAct(Vector3 start, GameObject plane, float width, byte sqsZDir, byte sqsXDir, 
                            byte sizeSmall, float relSpcBtwnSqs, Action<GameObject, byte, byte, byte, byte> extraAct)
    {
        //A, B, C variables method -- here for reminding/remembering what each variable does
        // int a ~ sqsZDir;     //length in z direction
        // int b ~ sqsXDir;     //length in x direction
        // int c ~ sizeSmall;     //small square dimensions (c by c)

        float widthPlane = width * 10; // plane is 10 x 10 default
        float spaceSmall = widthPlane;  //distance to move to start the next square
        float spaceLarge = widthPlane * sizeSmall * (1 + relSpcBtwnSqs);

        // c = xSmall & zSmall loop, b = x loop, a = z loop
        for (byte x = 0; x < sqsXDir; x++)
        {
            for (byte z = 0; z < sqsZDir; z++)
            {
                for (byte xSmall = 0; xSmall < sizeSmall; xSmall++)
                {
                    for (byte zSmall = 0; zSmall < sizeSmall; zSmall++)
                    {
                        plane.transform.localScale = new Vector3(width, width, width);   //size

                        //For position, move only first and last coordinates
                        float posX = start.x + spaceLarge * x + spaceSmall * xSmall;
                        float posZ = start.z + spaceLarge * z + spaceSmall * zSmall;

                        //initial position is 'start'
                        GameObject objMade = 
                            Instantiate(plane, new Vector3(posX, start.y, posZ), Quaternion.identity);

                        extraAct(objMade, x, z, xSmall, zSmall);



                    }

                }
            }
        }


    }
}
