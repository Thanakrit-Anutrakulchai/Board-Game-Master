using UnityEngine;
using UnityEngine.UI;
// type alias, (# of players, # of rows, # of cols, piece resolution)
using DimensionsData = System.Tuple<byte, byte, byte, byte>;

// Items for entering the dimensions (of board) specification process
internal sealed class ChooseBoardDim : IAssociatedState<Object, DimensionsData>
{
    [SerializeField] internal Canvas canvas;

    [SerializeField] internal Button useDimsButton;
    [SerializeField] internal InputField numPlayersInput;
    [SerializeField] internal InputField numRowsInput;
    [SerializeField] internal InputField numColsInput;
    [SerializeField] internal InputField pceResInput;
    [SerializeField] internal Slider gapSlider; // moved from MakeBoard 



    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseBoardDim;
    }


    public void OnEnterState<G>(IAssociatedState<G, Object> prev, Object args) 
    { 
        // TODO
    }


    /// <summary>
    /// pass user input to Make Game process
    /// </summary>
    /// <returns>user input information</returns>
    public DimensionsData OnLeaveState<G>(IAssociatedState<DimensionsData, G> _) 
    {
        // NOTE: variables can be declared right as they are used in C#
        //   so byte b; f(b); ~ f(byte b);    That's pretty neat
        //   also, 'out' just means the variable is passed/returned by reference

        // parses user input, move on to next process if inputs are valid
        if (byte.TryParse(numPlayersInput.text, out byte numPlayers) &&
            byte.TryParse(numRowsInput.text, out byte numRows) &&
            byte.TryParse(numColsInput.text, out byte numCols) &&
            byte.TryParse(pceResInput.text, out byte pceRes))
        {
            // TODO check input is valid
            return System.Tuple.Create(numPlayers, numRows, numCols, pceRes);
        }
        else 
        {
            // TODO Add checks -> keep going until success
            return null;
        }
    }
}
