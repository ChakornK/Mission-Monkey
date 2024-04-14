using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LemonStudios.UI
{
    public static class LemonUIUtils
    {
        public static void SwitchMenus(GameObject menuToHide, GameObject menuToShow)
        {
            menuToHide.SetActive(false);
            menuToShow.SetActive(true);
        }

        public static Color CreateNormalizedColor(int r, int g, int b, int a = 255)
        {
            float normalizedR = ColorNormalizer(r);
            float normalizedG = ColorNormalizer(g);
            float normalizedB = ColorNormalizer(b);
            float normalizedA = ColorNormalizer(a);
            return new Color(normalizedR, normalizedG, normalizedB, normalizedA);
        }

        public static float ColorNormalizer(int input)
        {
            return Mathf.Max(0, Mathf.Min(255, (float) input) / 255);
        }
        
        public static IEnumerator SmoothlyUpdateFillUI(Image targetGraphic, float targetFillAmount)
        {
            while (Math.Abs(targetGraphic.fillAmount - targetFillAmount) > 0.0001f)
            {
                float currentFill = targetGraphic.fillAmount;
                float loopFill = Mathf.Lerp(currentFill, targetFillAmount, Time.deltaTime * 0.2f);
                targetGraphic.fillAmount = loopFill;
                
                yield return new WaitForEndOfFrame();
            }
        }
   
        
        // Use Mathf.Lerp() to smoothly change the alpha of an image
        public static IEnumerator SmoothAlphaUpdate(Image targetGraphic, float targetAlpha, float animationDuration)
        {
            Color graphicColor = targetGraphic.color;
            float originalAlpha = targetGraphic.color.a;
            float currentTime = 0;
            
            while (currentTime < animationDuration) 
            {
                currentTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(originalAlpha, targetAlpha, currentTime / animationDuration);
                targetGraphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, newAlpha);

                yield return new WaitForEndOfFrame();
            }
        }
        
        
        // Exact same method as above but for TextMeshProUGUI elements instead of an image
        public static IEnumerator SmoothAlphaUpdate(TextMeshProUGUI targetGraphic, float targetAlpha, float animationDuration)
        {
            Color graphicColor = targetGraphic.color;
            float originalAlpha = targetGraphic.color.a;
            float currentTime = 0;
            
            while (currentTime < animationDuration) 
            {
                currentTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(originalAlpha, targetAlpha, currentTime / animationDuration);
                targetGraphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, newAlpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
