using System;

// provides extension methods for arrays, to make handling them easy
public static class ArrayExtensions
{
    /*** EXTENSION METHODS -- ARRAY ***/
    // replace all 'null' elements in a 2D array with value provided by supplier
    public static void FillWith<T>(this T[,] arr, Func<int, int, T> supplier)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                if (arr[i, j] == null) // never true for value types (?)
                {
                    arr[i, j] = supplier(i, j);
                }
            }
        }
    }



    // set every unaffected area to object of type SquareChange.Unaffected 
    //   this is to speed up computation of application of rule during game play
    public static void NoteUnaffected(this RuleInfo.SquareChange[,] changes) 
    { 
        for (int r = 0; r < changes.GetLength(0); r++) 
        { 
            for (int c = 0; c < changes.GetLength(1); c++) 
            {
                switch (changes[r, c]) 
                {
                    case RuleInfo.SquareChange.Unaffected un:
                        break; // do nothing for already unaffected objects
                    case RuleInfo.SquareChange.Changed changed:
                        // it is unaffected if it is a singleton list with
                        //   same piece as piece changed to
                        if (changed.pieceChangedFrom.Count == 1 &&
                            changed.pieceChangedFrom[0] == changed.pieceChangedTo)
                        {
                            changes[r, c] = new RuleInfo.SquareChange.Unaffected();
                        }
                        // or if changeFrom is empty (no piece) and 
                        //   changed to is also no piece
                        else if (changed.pieceChangedFrom.Count == 0 &&
                                 changed.pieceChangedTo == PieceInfo.noPiece) 
                        {
                            changes[r, c] = new RuleInfo.SquareChange.Unaffected();
                        }
                        // otherwise, keep as is
                        break;
                    default:
                        throw new ArgumentException("Unaccounted for SquareChange type");
                }
            }
        } // end of double for loop traversing changes 2D array
    }



    // replace all elements in a 2D array with value provided by supplier
    public static void ReplaceAllWith<T>(this T[,] arr, Func<int, int, T> supplier)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                arr[i, j] = supplier(i, j);
            }
        }
    }



    // checks whether there is a subarr in bigArr, after padding it, 
    //   such that when it is 'covered' with littleArr 
    // (applying 'cover' to both elements at that position) 
    //   all positions returns true
    public static bool Covers<L, B>(this L[,] littleArr, B[,] bigArr, 
                                    Func<L, B, bool> cover, B padding) 
    {
        UnityEngine.Debug.Log("CHECKING SUBARR COVERING");

        // uses padded array to check partial match
        IProvider2D<int, B> bigProv = bigArr.ToProvider().Pad
                                                (padding,
                                                 littleArr.GetLength(0) - 1,
                                                 littleArr.GetLength(1) - 1);

        // TODO MAKE THIS FASTER
        int lengthDiff0 = bigProv.GetLength(0) - littleArr.GetLength(0);
        int lengthDiff1 = bigProv.GetLength(1) - littleArr.GetLength(1);
        for (int i = 0; i <= lengthDiff0; i++)
        {
            for (int j = 0; j <= lengthDiff1; j++)
            {
                bool covers = CoversAt(littleArr, bigProv, i, j, cover);
                if (covers)
                {
                    UnityEngine.Debug.Log("SHOWN COVERED REACHED");
                    return true;
                }
            }
        } // end of quad for loop, checking all subarr spots

        return false;
    }



    // checks that littleArr 'covers' a sub-array of the same size in big prov.
    //   starting at the position specified, at a corner
    public static bool CoversAt<L, B>(this L[,] littleArr, IProvider2D<int, B> bigProv, 
                                      int posI, int posJ,
                                      Func<L, B, bool> cover)
    {
        bool shownDifferent = false;
        for (int r = 0; r < littleArr.GetLength(0); r++)
        {
            for (int c = 0; c < littleArr.GetLength(1); c++)
            {
                shownDifferent |= !cover(littleArr[r, c], bigProv[posI + r, posJ + c]);
            }
        } // end of inner double for loop, checking one subarr spot

        return !shownDifferent;
    }

    // overloaded version of CoversAt that works for bigProv
    public static bool CoversAt<L, B>(this L[,] littleArr, B[,] bigArr,
                                      int posI, int posJ,
                                      Func<L, B, bool> cover, B padding)
    {
        // uses padded array to check partial match
        IProvider2D<int, B> bigProv = bigArr.ToProvider().Pad
                                                (padding,
                                                 littleArr.GetLength(0) - 1,
                                                 littleArr.GetLength(1) - 1);

        return CoversAt(littleArr, bigProv, posI, posJ, cover);
    }



    // checks if there is a subarray of bigArr with the same elements as littleArr
    //   indexes where littleArr contains skipOn will not be checked
    public static bool IsSubMatrixOf<T>(this T[,] littleArr, T[,] bigArr, T skipOn)
        where T : IEquatable<T>
    {
        return littleArr.Covers(bigArr, MkIsSubMatrixCover(skipOn), skipOn);
    }


    // helper function -- creates covering for the Covers function
    // cover returns true iff. fromLil == skipOn || fromLil = fromBig
    private static Func<T, T, bool> MkIsSubMatrixCover<T>(T skipOn) where T : IEquatable<T> 
    {
        return
            (fromLil, fromBig) => (fromLil.Equals(skipOn)) || (fromLil.Equals(fromBig));
    }



    // returns an IProvider2D which just returns elements of the array at each index
    public static IProvider2D<int, T> ToProvider<T>(this T[,] arr)
    {
        return new Linked2D<T, T>(arr, Utility.Identity);
    }



    // returns another visual representation similar to the one given,
    //   with the alpha set to value specified
    public static PosInfo[,] WithAlpha(this PosInfo[,] visRep, byte alpha)
    {
        PosInfo[,] result = new PosInfo[visRep.GetLength(0), visRep.GetLength(1)];
        for (int r = 0; r < result.GetLength(0); r++)
        {
            for (int c = 0; c < result.GetLength(1); c++)
            {
                switch (visRep[r, c])
                {
                    case PosInfo.RGBData rgb: //RGBWithAlpha is dealt with here, too
                        PosInfo.RGBWithAlpha resRgba =
                            new PosInfo.RGBWithAlpha(rgb.red, rgb.blue, rgb.green, alpha);
                        result[r, c] = resRgba;
                        break;
                    case PosInfo.Nothing nothing:
                        result[r, c] = new PosInfo.Nothing();
                        break;
                }
            }
        } // end of double for loop, looping through result and visRep


        return result;
    }





    /*** EXTENSION METHODS -- IProvider2D ***/
    // returns a new, "padded" on all side Provider with value specified
    public static IProvider2D<int, T> Pad<T>(this IProvider2D<int, T> provider, 
                                             T padding, int padWidth, int padHeight) 
    {
        Func<int, int, T> getAtIndex = (int x, int y) =>
        {
            if (x.InRange(padWidth, provider.GetLength(0) + padWidth - 1) && 
                y.InRange(padHeight, provider.GetLength(1) + padHeight - 1))
            {
                return provider[x - padWidth, y - padHeight];
            }
            else
            {
                return padding;
            }
        };

        return getAtIndex.ToProvider(provider.GetLength(0) + (2*padWidth), 
                                     provider.GetLength(1) + (2*padHeight));
    }





    /*** EXTENSION METHODS -- FUNC<K, K, T> ***/
    // treats first two arguments as index and return value as value at that index
    //   provides lengths specified


    private class ToProviderWrapper<K, T> : IProvider2D<K, T> // helper inner class
    {
        private readonly Func<K, K, T> function;
        private readonly int length0;
        private readonly int length1;

        internal ToProviderWrapper(Func<K, K, T> fnc, int l0, int l1) 
        { function = fnc; length0 = l0; length1 = l1; }

        public T this[K key1, K key2] => function(key1, key2);
        public int GetLength(int n)
        {
            if (n == 0) 
            {
                return length0;
            } 
            else if (n == 1) 
            {
                return length1;
            }  
            else 
            {
                // errors on purpose for same type of error message
                return (new int[0, 0].GetLength(n));
            }
        }
    }

    public static IProvider2D<K, T> ToProvider<K, T>(this Func<K, K, T> func, int l0, int l1) 
    {
        return new ToProviderWrapper<K, T>(func, l0, l1);
    }





}
