using LemonStudios.Generic;
using LemonStudios.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubtitlesUI : MonoBehaviour
{
    private Image subtitlesUIImage;
    private TextMeshProUGUI subtitlesText;
    public bool subtitlesEnabled;
    
    private void Start()
    {
        var subtitlesUI = GameObject.FindGameObjectWithTag("SubtitlesRoot");
        subtitlesUIImage = subtitlesUI.GetComponent<Image>();
        subtitlesText = subtitlesUI.GetComponentInChildren<TextMeshProUGUI>();
        
        // subtitlesEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("SubtitlesMode"));
        HideSubtitles();
    }

    public void ShowSubtitles(string initialDialogue, float showTime = 0.15f)
    {
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 1, showTime));
        subtitlesText.text = formatDialogueText(initialDialogue);
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 1, showTime));
    }

    public void UpdateSubtitles(string updatedSubtitles, float updateTime = 0.15f)
    {
        // Fades out in half the time inputted, fades in within half the time inputted
        float splitUpdateTime = updateTime / 2;
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, splitUpdateTime));
        subtitlesText.text = formatDialogueText(updatedSubtitles);;
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 1, splitUpdateTime));
    }
    
    public void HideSubtitles(float hideTime = 0.15f)
    {
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 0, hideTime));
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, hideTime));
        subtitlesText.text = "null";
    }


    public bool areSubtitlesHidden()
    {
        return subtitlesUIImage.color.a == 0;
    }

    private string formatDialogueText(string input)
    {
        string[] characterNameSplit = input.Split(":");
        string characterName = characterNameSplit[0];
        
        switch (characterName)
        {
            case "Shob3r":
                characterName = "<color=#7D18C9>" + characterName + ":</color>";
                break;
            case "Debug":
                characterName = "<color=#47A9FF>" + characterName + ":</color>";
                break;
            case "Anti-Org Comms":
                characterName = "<color=#9EF230>" + characterName + ":</color>";
                break;
        }
        
        string rebuiltString = characterName;
        for (int i = 1; i < characterNameSplit.Length; i++)
        {
            rebuiltString += characterNameSplit[i];
        }
        
        return rebuiltString;
    }
}