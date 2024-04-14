using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
            
            Debug.LogWarning("List " + list + " only contains empty elements!");
            throw new Exception();
        }
    }
}