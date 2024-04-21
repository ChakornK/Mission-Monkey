using UnityEngine;

public class OnSceneLoad : SaveDataBase
{
    private void Start()
    {
        // Move the player to the correct position if the game can tell that the scene get loaded by SaveData
        Time.timeScale = 1;
        if(lastLoadFromSaveData)
        {
            lastLoadFromSaveData = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterController playerController = player.GetComponent<CharacterController>();
            playerController.enabled = false;
    
            player.transform.position = GetPositionFromSaveData();
            playerController.enabled = true;
        }
    }
}
