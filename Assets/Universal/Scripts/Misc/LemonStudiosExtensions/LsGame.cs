using UnityEngine;

namespace LemonStudios.Game
{
    public static class LemonGameUtils
    {
        // I wonder what this method does guys
        public static bool IsGamePaused()
        {
            return Time.timeScale == 0;
        }
    }
}