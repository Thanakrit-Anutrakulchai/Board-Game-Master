using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// type alias, (# of players, # of rows, # of columns, )
using GameData = System.Tuple<byte, byte, byte, float, System.Collections.Generic.List<PieceInfo>>;


// script which controls the behaviour of the game creation process 
public class GameCreationHandler : MonoBehaviour
{
    /*** STATIC METHODS ***/
    internal static GameCreationHandler GetHandler()
    {
        return Camera.main.GetComponent<GameCreationHandler>();
    }
}
