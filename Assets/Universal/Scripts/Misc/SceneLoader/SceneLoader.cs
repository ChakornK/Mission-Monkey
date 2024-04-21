using LemonStudios.Game;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // TODO: integrate this with GameUtils namespace (again, i'm an absolutely atrociously lazy programmer)
    
    // UI cannot just start coroutines by themselves 😔😔😔 more bloat to the code I guess (I'm probably being stupid right now)
    public void UISceneLoadByName(string sceneName)
    {
        StartCoroutine(LemonGameUtils.LoadSceneByName(sceneName));
    }

    public void UISceneLoadByBuildNumber(int buildNumber)
    {
        StartCoroutine(LemonGameUtils.LoadSceneByBuildNumber(buildNumber));

    }
}
