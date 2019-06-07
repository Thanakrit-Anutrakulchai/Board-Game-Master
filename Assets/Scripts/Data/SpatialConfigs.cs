using UnityEngine;

// class which stores configurations for the (relative) size and position of objects
public class SpatialConfigs // TODO init at start ?
{
    // location to move camera to at the start of each process
    internal static Vector3 commonCameraPosition = new Vector3(0, 100, 0);

    // y-distance (height) of board from the plane at y=0
    internal static float heightOfBoard = 10;

    /* value assigned in board creation handler currently used
    // default size of board square in units
    internal static float boardSquareSize = 4f;
    // /*
}
