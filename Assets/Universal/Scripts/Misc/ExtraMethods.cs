using System.IO;
using System.Linq;

namespace LemonStudios.CsExtensions
{
    public static class LemonStudiosCsExtensions
    {
        // https://stackoverflow.com/questions/3870263/how-can-i-write-like-x-either-1-or-2-in-a-programming-language 
        public static bool Either(this object value, params object[] array)
        {
            return array.Any(p => Equals(value, p));
        }

        public static bool DoesFileExist(string file)
        {
            if (File.Exists(file))
            {
                return true;
            }
            else return false;
        }
    }
}