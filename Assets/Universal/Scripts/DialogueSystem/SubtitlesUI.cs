using System;
using LemonStudios.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubtitlesUI : MonoBehaviour
{
    public bool enableDebugMessages;
    
    private Image subtitlesUIImage;
    private TextMeshProUGUI subtitlesText;
    private bool subtitlesEnabled;
    
    private void Start()
    {
        var subtitlesUI = GameObject.FindGameObjectWithTag("SubtitlesRoot");
        subtitlesUIImage = subtitlesUI.GetComponent<Image>();
        subtitlesText = subtitlesUI.GetComponentInChildren<TextMeshProUGUI>();
        
        subtitlesEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("SubtitlesMode"));

        // I HATE UNITY'S COLOR CLASS OH MY GOD
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 0, 0.00001f));
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, 0.00001f));
    }

    public void ShowSubtitles(string initialDialogue, float showTime = 0.15f)
    {
        if (subtitlesEnabled)
        {
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 1, showTime));
            subtitlesText.text = formatDialogueText(initialDialogue);
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 1, showTime));
        }
        else if(enableDebugMessages)
        {
            Debug.Log("Subtitles Not Enabled, ShowSubtitles will not run");
        }
    }

    public void UpdateSubtitles(string updatedSubtitles, float updateTime = 0.15f)
    {
        if (subtitlesEnabled)
        {
            // Fades out in half the time inputted, fades in within half the time inputted
            float splitUpdateTime = updateTime / 2;
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, splitUpdateTime));
            subtitlesText.text = formatDialogueText(updatedSubtitles);
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 1, splitUpdateTime));
        }
        
        else if(enableDebugMessages)
        {
            Debug.Log("Subtitles Not Enabled, UpdateSubtitles will not run");
        }
    }
    
    public void HideSubtitles(float hideTime = 0.15f)
    {
        if (subtitlesEnabled)
        {
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 0, hideTime));
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, hideTime));
            subtitlesText.text = string.Empty;
        }
        else if(enableDebugMessages)
        {
            Debug.Log("Subtitles Not Enabled, HideSubtitles will not run");
        }
    }

    public bool areSubtitlesHidden()
    {
        return subtitlesUIImage.color.a == 0;
    }
    
    public void setSubtitlesStatus(bool state)
    {
        subtitlesEnabled = state;
    }
    
    // TODO: Make this method a separate class and make it better
    private string formatDialogueText(string input)
    {
        string[] characterNameSplit = input.Split(":");
        string characterName = characterNameSplit[0];
        
        switch (characterName)
        {
            case "Shob3r":
                characterName = $"<color=#7D18C9>{characterName}:</color>";
                break;
            case "Debug":
                characterName = $"<color=#47A9FF>{characterName}:</color>";
                break;
            case "Anti-Org Comms":
                characterName = $"<color=#9EF230>{characterName}:</color>";
                break;
        }
        
        string rebuiltString = characterName;
        // Start index at 1 because index 0 is the character dialogue
        // For loop exists in case that the dialogue line itself contains colons (which would split the string apart more)
        for (int i = 1; i < characterNameSplit.Length; i++)
        {
            rebuiltString += characterNameSplit[i];
        }
        return rebuiltString;
    }
}