using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Right now, none of the methods here are used, but it's useful to have in case I ever DO need them in the future!
// If not, I can just distribute this as a sparete library since this namespace is just C# extensions
namespace LemonStudios.Generic
{
    public static class LemonGenericUtils
    {
        public static bool DoesFileExist(string file)
        {
            return File.Exists(file);
        }

        public static int GetFirstNonNullEntryOfList<T>(List<T> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i] != null)
                {
                    return i;
                }
            }
            
            Debug.LogWarning($"List {list} only contains empty elements!");
            throw new Exception();
        }

        public static string ConvertToHex(int input)
        {
            return input.ToString("X");
        }

        public static string intArrToHex(int[] input, bool containSpaces = false)
        {
            string output = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                // i != 0 to prevent from there being a space at the very start of the string
                if (containSpaces && i != 0)
                {
                    output += " " + input[i].ToString("X");
                }
                else
                {
                    output += input[i].ToString("X");
                }
            }
            return output;
        }
    }
}