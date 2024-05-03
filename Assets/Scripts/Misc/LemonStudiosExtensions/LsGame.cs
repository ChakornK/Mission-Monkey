using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LemonStudios.Game
{
    public static class LemonGameUtils
    {
        // I wonder what this method does guys
        public static bool IsGamePaused()
        {
            return Time.timeScale == 0;
        }
        
        // Keeping both of these methods public in case I want to use them elsewhere later
        public static IEnumerator LoadSceneByName(string sceneName)
        {
            // Stol- I mean borrowed directly from the unity documentation. Loads a scene asynchronously because that's supposedly the correct way to do things right now
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);

            while (!loadScene.isDone)
            {
                yield return null;
            }
        }
    
        public static IEnumerator LoadSceneByBuildNumber(int sceneBuildNumber)
        {
            // Same gist as the method above, but this time use the build number of the scene instead of the scene name
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneBuildNumber);

            while (!loadScene.isDone)
            {
                yield return null;
            }
        }
    }
}