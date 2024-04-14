using System;
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
        subtitlesUIImage = GameObject.FindGameObjectWithTag("SubtitlesRoot").GetComponent<Image>();
        subtitlesEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("SubtitlesMode"));
    }

    public void ShowSubtitles(float showTime = 0.25f)
    {
        if (!subtitlesEnabled) return;
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 1, showTime));
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 1, showTime));
    }

    public void UpdateSubtitles(string updatedSubtitles, float updateTime = 0.25f)
    {
        if (!subtitlesEnabled) return;
        // Fades out in half the time inputted, fades in within half the time inputted
        float splitUpdateTime = updateTime / 2;
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, splitUpdateTime));
        subtitlesText.text = updatedSubtitles;
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 1, splitUpdateTime));
    }
    
    public void HideSubtitles(float hideTime = 0.25f)
    {
        if (!subtitlesEnabled) return;
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesUIImage, 0, hideTime));
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(subtitlesText, 0, hideTime));
    }
}