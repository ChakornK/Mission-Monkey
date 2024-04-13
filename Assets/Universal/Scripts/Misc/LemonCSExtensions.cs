using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LemonStudios.CsExtensions
{
    public static class LemonUtilsGeneric
    {
        public static bool DoesFileExist(string file)
        {
            return File.Exists(file);
        }

        public static int GetFirstNonNullEntryOfList<T>(List<T> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i] != null)
                {
                    return i;
                }
            }
            
            Debug.LogWarning("List " + list + " only contains empty elements!");
            throw new Exception();
        }
    }

    public static class LemonGameUtils
    {
        public static bool IsGamePaused()
        {
            return Time.timeScale == 0;
        }
    }

    public static class LemonUIUtils
    {
        public static void SwitchMenus(GameObject menuToHide, GameObject menuToShow)
        {
            menuToHide.SetActive(false);
            menuToShow.SetActive(true);
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
