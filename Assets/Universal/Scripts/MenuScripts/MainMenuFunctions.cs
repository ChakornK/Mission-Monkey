using LemonStudios.CsExtensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    public SaveData saveData;
    [Tooltip("Only assign on game scenes, not main menu")]
    public GameObject gameSavedPopup;
    [Tooltip("Only assign on game scenes, not main menu")]
    public OpenPauseMenu pauseMenuMethods;

    [Space]
    public Slider volumeSlider, mouseSensitivitySlider, fieldOfViewSlider;
    public TMP_Dropdown qualityDropdown, antiAliasingDropdown, subtitlesDropdown;
    public TextMeshProUGUI volumePercentageText, mouseSensitivityText, fieldOfViewText;
    [Space]
    public Camera playerCamera;
    public PlayerLook PlayerLook;
    [Space]
    public UniversalAdditionalCameraData urpAdditionalCameraData;
    public AudioMixer mainVolume;

    private void Start()
    {
        LoadSettingsValues();
    }

    public void LoadSettingsValues()
    {
        // Load volume value from a previous session
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        SetVolume(volumeSlider.value);

        // Load Mouse Sensitivity from a previous session
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivityValue");
        SetMouseSensitivty(mouseSensitivitySlider.value);

        // Load FOV from a previous session
        fieldOfViewSlider.value = PlayerPrefs.GetFloat("Fov");
        SetCameraFov(fieldOfViewSlider.value);

        // Load Quality Mode from a previous session
        qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        SetQuality(qualityDropdown.value);

        // Load Anti-Aliasing mode from a previous session
        antiAliasingDropdown.value = PlayerPrefs.GetInt("AntiAliasing");
        SetAntiAliasing(antiAliasingDropdown.value);
    }

    public void SetVolume(float volume)
    {
        int TextDisplayVolume = Mathf.FloorToInt(volume * 100);
        // I have no idea how this script calculates volume percentage but it works so i do not care
        mainVolume.SetFloat("Volume", Mathf.Log10(volume) * 20);
        volumePercentageText.text = TextDisplayVolume.ToString() + "%";
        PlayerPrefs.SetFloat("Volume", volume);

        ///Debug.Log("Set volume to" + volume * 100);
    }

    public void SetMouseSensitivty(float MouseSens)
    {
        // Stolen code from the old SettingsMenu.cs script. It should work
        PlayerLook.setMouseSensitivity(MouseSens);
        if (mouseSensitivitySlider.value != MouseSens)
        {
            mouseSensitivitySlider.value = MouseSens;
        }

        mouseSensitivityText.text = Mathf.CeilToInt(MouseSens).ToString();
        PlayerPrefs.SetFloat("MouseSensitivityValue", MouseSens);
    }

    public void SetCameraFov(float CameraFov)
    {
        playerCamera.fieldOfView = CameraFov;
        fieldOfViewText.text = Mathf.FloorToInt(CameraFov).ToString();
        PlayerPrefs.SetFloat("Fov", CameraFov);
    }

    public void SetQuality(int QualityPreset)
    {
        // Quality Mode is based off of how the quality is ordered in the project settings
        // QualityPreset = qualityDropdown.value;
        QualitySettings.SetQualityLevel(QualityPreset);
        PlayerPrefs.SetInt("QualityLevel", QualityPreset);
        // Debug.Log("Set Quality to: " + QualitySettings.GetQualityLevel().ToString());
    }

    private void SetCaptions()
    {
        // For 0.4
    }

    public void SetAntiAliasing(int AntiAliasingValue)
    {
        // Using a switch case (the value of which is decided through the Anti-Aliasing Dropdown), the Anti Aliasing gets set to either Off, FXAA, TAA, or SMAA
        // AntiAliasingValue = antiAliasingDropdown.value;
        switch (AntiAliasingValue)
        {
            case 0:
                urpAdditionalCameraData.antialiasing = AntialiasingMode.None;
                break;
            case 1:
                urpAdditionalCameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                break;
            case 2:
                urpAdditionalCameraData.antialiasing = AntialiasingMode.TemporalAntiAliasing;
                break;
            case 3:
                urpAdditionalCameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                break;
        }
        // Debug.Log("Setting Anti Aliasing to" + urpAdditionalCameraData.antialiasing);

        PlayerPrefs.SetInt("AntiAliasing", AntiAliasingValue);
    }


    public void newGame(string sceneName)
    {
        if (LemonStudiosCsExtensions.DoesFileExist(saveData.GetSaveDataLocation()))
        {
            saveData.DeleteSaveData();
            LoadNewScene(sceneName);
        }
        else LoadNewScene(sceneName);
    }

    public void WriteToSaveData()
    {
        saveData.WriteSaveData();
        pauseMenuMethods.ResumeGame();
        pauseMenuMethods.IsOnPauseMenu -= 1; // I gotta clean up the pause menu code later
        ShowGUI(gameSavedPopup);
        StartCoroutine(waitUntilHideGUI(3.5f));
    }

    public void LoadNewScene(string scene)
    {
        if (Time.timeScale != 1) // For when the method is called from the pause menu
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(scene);
        if (scene == null) { Debug.LogError("Scene not properly specified on 1 or more objects"); }
    }

    private IEnumerator waitUntilHideGUI(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        HideGUI(gameSavedPopup);
    }

    public void ShowGUI(GameObject GuiToShow)
    {
        GuiToShow.SetActive(true);
    }

    public void HideGUI(GameObject GuiToHide)
    {
        GuiToHide.SetActive(false);
    }

    public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }

    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.None;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // Unlock cursor before game closes
        Cursor.lockState = CursorLockMode.None;
    }
}