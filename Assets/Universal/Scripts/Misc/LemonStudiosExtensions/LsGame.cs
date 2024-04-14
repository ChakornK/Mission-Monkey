using UnityEngine;

namespace LemonStudios.Game
{
    public static class LemonGameUtils
    {
        public static bool IsGamePaused()
        {
            return Time.timeScale == 0;
        }
    }
}