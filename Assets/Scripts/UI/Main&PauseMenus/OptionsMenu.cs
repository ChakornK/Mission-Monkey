using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mainVolume;
    public Camera mainCamera;
    public TextMeshProUGUI volumeValueText, mouseSensitivityValueText, fieldOfViewValueText;
    public TMP_Dropdown qualityDropdown, aaModeDropdown, vSyncDropdown, subtitlesDropdown;
    public TMP_Dropdown richPresenceDropdown;
    public Slider volumeSlider, fovSlider, mouseSensitivitySlider;
    public SubtitlesUI subtitlesUI;
    public DiscordRichPresenceController rpcController;
    private PlayerCamera playerCamera;
    private UniversalAdditionalCameraData urpCamData;
    private UniversalRenderPipelineAsset urpAsset;
    

    private void Awake()
    {
        mainCamera = Camera.main;
        
        // PlayerCamera is the script the controls the players camera inputs. not to be confused with a camera component (I should probably rename this honestly)
        if (mainCamera == null) return;
        playerCamera = mainCamera.GetComponentInParent<PlayerCamera>();
        urpCamData = mainCamera.GetComponent<UniversalAdditionalCameraData>();
        SetOptionsFromPlayerPrefs();
    }

    private void SetOptionsFromPlayerPrefs()
    {
        // Set the values of each dropdown/slider to their saved value, then call the method to update all the options using the values of said dropdowns 
        
        qualityDropdown.value = PlayerPrefs.GetInt("GraphicsQuality");
        SetGraphicsQuality(qualityDropdown.value);

        aaModeDropdown.value = PlayerPrefs.GetInt("AntiAliasingMode");
        SetAntiAliasing(aaModeDropdown.value);

        vSyncDropdown.value = PlayerPrefs.GetInt("VsyncMode");
        SetVSync(vSyncDropdown.value);
        
        subtitlesDropdown.value = PlayerPrefs.GetInt("SubtitlesMode");
        SetSubtitles(subtitlesDropdown.value);

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        SetVolume(volumeSlider.value);

        fovSlider.value = PlayerPrefs.GetFloat("FieldOfView");
        SetFieldOfView(fovSlider.value);

        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        SetMouseSensitivity(mouseSensitivitySlider.value);

        richPresenceDropdown.value = PlayerPrefs.GetInt("DiscordIntegration");
        SetRPCActiveness(richPresenceDropdown.value);
    }

    public void SetGraphicsQuality(int newQualityLevel)
    {
        QualitySettings.SetQualityLevel(newQualityLevel);
        PlayerPrefs.SetInt("GraphicsQuality", newQualityLevel);
    }

    public void SetAntiAliasing(int newAntiAliasingMode)
    {
        switch (newAntiAliasingMode)
        {
            case 0:     // OFF
                urpCamData.antialiasing = AntialiasingMode.None;
                break;
            case 1:     // FXAA
                urpCamData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                break;
            case 2:     // TAA
                urpCamData.antialiasing = AntialiasingMode.TemporalAntiAliasing;
                break;
            case 3:     // SMAA
                urpCamData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                break;
        }
        
        PlayerPrefs.SetInt("AntiAliasingMode", newAntiAliasingMode);
    }

    public void SetVSync(int vsyncMode)
    {
        QualitySettings.vSyncCount = vsyncMode;
        PlayerPrefs.SetInt("VsyncMode", vsyncMode);
    }
    
    public void SetSubtitles(int subtitlesMode)
    {
        if (subtitlesUI != null)
        {
            subtitlesUI.setSubtitlesStatus(Convert.ToBoolean(subtitlesMode));
        }
        
        PlayerPrefs.SetInt("SubtitlesMode", subtitlesMode);
    }

    public void SetVolume(float volume)
    {
        mainVolume.SetFloat("Volume", Mathf.Log10(volume) * 20);
        int textDisplayVolume = Mathf.RoundToInt(volume * 100);
        volumeValueText.text = textDisplayVolume + "%";
        
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetMouseSensitivity(float newMouseSensitivity)
    {
        int roundedMouseSensitivity = Mathf.RoundToInt(newMouseSensitivity);
        if (playerCamera != null)
        {
            playerCamera.SetSensitivity(roundedMouseSensitivity);
        }

        mouseSensitivityValueText.text = roundedMouseSensitivity.ToString();
        PlayerPrefs.SetFloat("MouseSensitivity", roundedMouseSensitivity);
    }

    public void SetFieldOfView(float newFovValue)
    {
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = newFovValue;
        }
        int roundedFovValue = Mathf.RoundToInt(newFovValue);
        fieldOfViewValueText.text = roundedFovValue.ToString();

        PlayerPrefs.SetFloat("FieldOfView", newFovValue);
    }

    public void SetRPCActiveness(int activeness)
    {
        // Inverse the activeness conversion because "on" is in slot 0 and 0 is false on booleans and yap yap yap yap yap
        bool convertedActiveness = !Convert.ToBoolean(activeness);
        rpcController.enableRichPresence = convertedActiveness;

        if (!convertedActiveness)
        {
            rpcController.DisposeRPC();
        }
        
        PlayerPrefs.SetInt("DiscordIntegration", activeness);
    }
}
