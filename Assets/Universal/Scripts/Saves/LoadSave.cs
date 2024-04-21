using LemonStudios.Game;

public class LoadSave : SaveDataBase
{
    public void LoadSaveData()
    {
        int lastSavedSceneBuildIndex = GetSaveDataInfoFromTag<int>("savedSceneBuildNumber");

        // Load the saved scene
        StartCoroutine(LemonGameUtils.LoadSceneByBuildNumber(lastSavedSceneBuildIndex));
        lastLoadFromSaveData = true;
    }
}
