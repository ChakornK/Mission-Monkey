using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class GlitchUI : MonoBehaviour
{
    public bool isGlitchingActive = true;
    public TextMeshProUGUI[] glitchedTextElements;
    public TMP_FontAsset[] fonts;


    private void Awake()
    {
        glitchedTextElements = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
        StartCoroutine(flickerFontAsset());
    }

    public IEnumerator flickerFontAsset()
    {
        while (isGlitchingActive)
        {
            foreach (TextMeshProUGUI textElement in glitchedTextElements)
            {
                if (textElement != null && textElement.gameObject.activeSelf)
                {
                    textElement.font = fonts[Random.Range(0, 1)];
                }
            }

            float randomTimeWait = Random.Range(0.05f, 0.085f);
            yield return new WaitForSeconds(randomTimeWait);
        }
    }
    
}
