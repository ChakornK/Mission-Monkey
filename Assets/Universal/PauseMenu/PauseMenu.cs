using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public void PauseGame () {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<InputManager>().OnDisable();
    }
    public void ResumeGame () {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<InputManager>().OnEnable();
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void TogglePause () {
        gameIsPaused = !gameIsPaused;
        pauseMenu.SetActive(gameIsPaused);
        if (gameIsPaused) {
            PauseGame();
        } else {
            ResumeGame();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}