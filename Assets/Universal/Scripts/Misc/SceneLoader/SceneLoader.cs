using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // UI cannot just start coroutines by themselves 😔😔😔 more bloat to the code I guess (I'm probably being stupid right now)
    
    public void UISceneLoadByName(string sceneName)
    {
        StartCoroutine(LoadSceneByName(sceneName));
    }

    public void UISceneLoadByBuildNumber(int buildNumber)
    {
        StartCoroutine(LoadSceneByBuildNumber(buildNumber));
    }
    
    // Keeping both of these methods public in case I want to use them elsewhere later
    public IEnumerator LoadSceneByName(string sceneName)
    {
        // Stol- I mean borrowed directly from the unity documentation. Loads a scene asynchronously because that's supposedly the correct way to do things right now
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);

        while (!loadScene.isDone)
        {
            yield return null;
        }
    }
    
    public IEnumerator LoadSceneByBuildNumber(int sceneBuildNumber)
    {
        // Same gist as the method above, but this time use the build number of the scene instead of the scene name
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneBuildNumber);

        while (!loadScene.isDone)
        {
            yield return null;
        }
    }
}
