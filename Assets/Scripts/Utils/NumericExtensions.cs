// adds extension methods to int and byte data types to make handling them easier
// Note that the operations are performed by casting the bytes to ints
//  this shouldn't be much slower as calculations on ints should be faster
//  due to the most likely CPU architecture (I believe?) 
public static class NumericExtensions
{
    /*** EXTENSIONS METHODS -- BYTE ***/
    // ensures that addition of two bytes is still within range to be a byte
    //  that is, returns true and result = b1 + b2 if their sum <= 255
    //  otherwise, returns false (and 255 for result) 
    public static bool AddCheck(this byte b1, byte b2, out byte result)
    {
        int sum = b1 + b2;
        if (sum > 255)
        {
            result = 255;
            return false;
        }
        else
        {
            result = (byte)sum;
            return true;
        }
    }



    // true iff number is in [low, high] (that is, low <= number <= high)
    public static bool InRange(this byte theNumber, byte low, byte high)
    {
        return (low <= theNumber) && (theNumber <= high);
    }



    // ensures that addition of two bytes is still within range to be a byte
    //  that is, returns true and result = b1 - b2 if their difference >= 0
    //  otherwise, returns false 
    public static bool SubCheck(this byte b1, byte b2, out byte result)
    {
        int diff = b1 - b2;
        if (diff < 0)
        {
            result = 255;
            return false;
        }
        else
        {
            result = (byte)diff;
            return true;
        }
    }





    /*** EXTENSION METHODS -- INT ***/
    // true iff number is in [low, high] (that is, low <= number <= high)
    public static bool InRange(this int theNumber, int low, int high) 
    {
        return (low <= theNumber) && (theNumber <= high);
    }

}
