using UnityEngine;
using UnityEngine.UI;
// type alias, (# of players, # of rows, # of cols, piece resolution)
using DimensionsData = System.Tuple<byte, byte, byte, byte>;

// Items for entering the dimensions (of board) specification process
internal sealed class ChooseBoardDim : Process<ChooseBoardDim>, 
    IAssociatedState<UnityEngine.Object, DimensionsData>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal readonly Canvas canvas;

    [SerializeField] internal readonly Button useDimsButton;
    [SerializeField] internal readonly InputField numPlayersInput;
    [SerializeField] internal readonly InputField numRowsInput;
    [SerializeField] internal readonly InputField numColsInput;
    [SerializeField] internal readonly InputField pceResInput;
    [SerializeField] internal readonly Slider gapSlider; // moved from MakeBoard 






    /*** INSTANCE METHODS ***/
    public Canvas GetCanvas() { return canvas; }
    public ProgramData.State GetAssociatedState()
    {
        return ProgramData.State.ChooseBoardDim;
    }


    public void OnEnterState(IAssociatedStateLeave<UnityEngine.Object> prev, UnityEngine.Object args) 
    {
        SetupUIs();
    }



    /// <summary>
    /// pass user input to Make Game process
    /// </summary>
    /// <returns>user input information</returns>
    public DimensionsData OnLeaveState(IAssociatedStateEnter<DimensionsData> _) 
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



    // clears old data in input fields
    private void SetupUIs()
    {
        numPlayersInput.text = "";
        numRowsInput.text = "";
        numColsInput.text = "";
        pceResInput.text = "";
    }
}
