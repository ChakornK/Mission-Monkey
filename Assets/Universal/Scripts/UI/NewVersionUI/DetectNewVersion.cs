using UnityEngine;

public class DetectNewVersion : SaveDataBase
{
    public GameObject newVersionUI;
    private void Start()
    {
        // Show the new version ui if the last played version in the SaveData does not equal the version of the running game or if the save data does not exist
        if (GetSaveDataInfoFromTag<string>("lastPlayedVersion") != Application.version || !DoesSaveDataFileExist())
        {
            newVersionUI.SetActive(true);
        }
    }
}
