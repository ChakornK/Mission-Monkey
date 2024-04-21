using System;
using System.Collections;
using LemonStudios.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FadeOutOnEnable : MonoBehaviour
{
    public float waitTime = 1.5f;
    public float fadeOutTime = 0.5f;
    private Image assetRoot;
    private TextMeshProUGUI assetText;

    private void Awake()
    {
        assetRoot = GetComponent<Image>();
        assetText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(waitUntilFadeOut());
    }

    private IEnumerator waitUntilFadeOut()
    {
        // Wait for the specified amount of time before starting to fade out
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(assetRoot, 0, fadeOutTime));
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(assetText, 0, fadeOutTime));
        
        // Wait until everything has faded out to continue the coroutine
        yield return new WaitForSeconds(fadeOutTime);
        
        // After fading out, set the alpha values back to max (this is the most convoluted method ever
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(assetRoot, 1, 0.0001f));
        StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(assetText, 1, 0.0001f));
        
        // Finally, disable the GameObject containing this script
        gameObject.SetActive(false);
    }
}
