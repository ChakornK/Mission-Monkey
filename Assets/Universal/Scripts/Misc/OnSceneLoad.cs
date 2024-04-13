using UnityEngine;

public class OnSceneLoad : SaveDataBase
{
    private void Start()
    {
        Time.timeScale = 1;
        if(IsLastLoadFromSaveData)
        {
            IsLastLoadFromSaveData = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterController playerController = player.GetComponent<CharacterController>();
            playerController.enabled = false;
    
            player.transform.position = GetPositionFromSaveData();
            playerController.enabled = true;
        }
    }
}
