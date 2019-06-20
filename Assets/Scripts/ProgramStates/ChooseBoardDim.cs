using UnityEngine;
using UnityEngine.UI;
// type alias, (# of players, # of rows, # of cols, piece resolution, relative gap size)
using DimensionsData = System.Tuple<byte, byte, byte, byte, float>;

// Items for entering the dimensions (of board) specification process
internal sealed class ChooseBoardDim : Process<ChooseBoardDim>, 
    IAssociatedState<UnityEngine.Object, DimensionsData>
{
    /*** INSTANCE VARIABLES ***/
    [SerializeField] internal Canvas canvas;
    
    [SerializeField] internal Button useDimsButton;
    [SerializeField] internal InputField numPlayersInput;
    [SerializeField] internal InputField numRowsInput;
    [SerializeField] internal InputField numColsInput;
    [SerializeField] internal InputField pceResInput;
    [SerializeField] internal Slider gapSlider; // moved from MakeBoard  
    [SerializeField] internal Text complainText;   





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
        bool validInput = byte.TryParse(numPlayersInput.text, out byte numPlayers) && 
            numPlayers.InRange(1, 250);
        validInput &= byte.TryParse(numRowsInput.text, out byte numRows) &&
            numRows.InRange(1, 250);
        validInput &= byte.TryParse(numColsInput.text, out byte numCols) &&
            numCols.InRange(1, 250);
        validInput &= byte.TryParse(pceResInput.text, out byte pceRes) &&
            pceRes.InRange(1, 250);
            
        if (validInput)
        {
            float gap = gapSlider.normalizedValue;
            return System.Tuple.Create(numPlayers, numRows, numCols, pceRes, gap);
        }
        else 
        {
            // complains that value is invalid
            complainText.text = "Please only enter whole numbers between 1 and 250";

            // stops the transition
            TransitionHandler.GetHandler().AbortTransition();

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

        // reset complain text
        complainText.text = "";
    }
}
