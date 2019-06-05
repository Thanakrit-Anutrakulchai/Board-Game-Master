// class representing information about the state of a board

[System.Serializable]
public class BoardInfo
{
    /*** INSTANCE VARIABLES ***/
    // board sizes in number of squares (which can have full pieces on top)
    public byte numOfRows;
    public byte numOfCols;
    public float squareSize; // size of the board square 

    // relative size of gaps between large squares, 
    //   e.g. 1 means gaps are as large as the squares
    public float sizeOfGap;
     

    // array representating the shape and colouring of the board
    public PosInfo[,] boardShapeRepresentation;


    // array representing which type of piece is where on the board
    // NOTE: array should always be of size numOfRows x numOfCold
    public byte[,] boardStateRepresentation;



    /*** INSTANCE PROPERTIES ***/
    // dimensions of the board in Unity's units
    public float Height
    {
        get
        {
            return numOfRows * squareSize + // space taken by squares
                   (numOfRows - 1) * sizeOfGap * squareSize; // taken by gaps 
        }
    }
    public float Width
    {
        get
        {
            return numOfCols * squareSize + // space taken by squares
                   (numOfCols - 1) * sizeOfGap * squareSize; // taken by gaps
        }
    }



    /*** CONSTRUCTORS ***/
    // hidden default constructor
    private BoardInfo() { }

    // generates board of numRows x numCols size without any pieces
    //  with large squares of size specified, and gaps of size specified
    private BoardInfo(byte numRows, byte numCols, 
                      float sqSize, float gapSize) 
    {
        // TODO
        // TEMP. debug messages
        UnityEngine.Debug.Log("ATTEMPTING TO MAKE BOARD WITH " + numRows +" ROWS");
        UnityEngine.Debug.Log("ATTEMPTING TO MAKE BOARD WITH " + numCols + " COLUMNS");


        this.numOfRows = numRows;
        this.numOfCols = numCols;
        this.sizeOfGap = gapSize;
        this.squareSize = sqSize;
        this.boardShapeRepresentation = PosInfo.NothingMatrix(numRows,numCols);
        this.boardStateRepresentation = new byte[numRows, numCols];
    }



    /*** STATIC METHODS ***/
    // creates a numRows x numCols board with specified colour,
    //  with no pieces on the board
    public static BoardInfo DefaultBoard(byte numRows, byte numCols, float sqSize,
                                         float gapSize, PosInfo.RGBData colour) 
    {
        // instantiates variables
        BoardInfo board = new BoardInfo(numRows, numCols, sqSize, gapSize);

        // sets all squares to be filled with that colour and no pieces
        for (byte r = 0; r < numRows; r++) 
        {
            for (byte c = 0; c < numCols; c++) 
            {
                board.boardShapeRepresentation[r,c] = colour;
                board.boardStateRepresentation[r,c] = PosInfo.noPiece;
            }
        }


        return board;
    }




    /*** INSTANCE METHODS ***/
    // CAREFUL! Returns by-value copy BUT with same pointer for ShapeRepresentation
    //  i.e. result.boardShapeRepresentation == this.boardShapeRepresentation still
    public BoardInfo GetCopy() 
    {
        // assigns all variables 
        BoardInfo result = new BoardInfo();
        result.numOfRows = this.numOfRows;
        result.numOfCols = this.numOfCols;
        result.squareSize = this.squareSize;
        result.sizeOfGap = this.sizeOfGap;
        result.boardShapeRepresentation = this.boardShapeRepresentation; //same ref

        // copies state representation by value
        result.boardStateRepresentation = 
            this.boardStateRepresentation.Clone() as byte[,];

        return result;
    }

    // returns true and assign piece at position r,c to 'piece'
    //  OR return false if unsuccessful, e.g. index out of bound
    //  Note that even if indexing succeeds, this may still return false,
    //   due to the lack of a board square at that index
    public bool TryGetPiece(byte r, byte c, out byte piece) 
    { 
        try 
        {
            PosInfo pieceRep = boardShapeRepresentation[r, c];
            piece = boardStateRepresentation[r, c];

            // guards against case where there is no board square there
            if (pieceRep is PosInfo.Nothing) 
            {
                return false;
            }

            return true;
        }  
        catch 
        {
            piece = 0; // piece must be assigned, unfortunately
            return false;
        }
    }
}
