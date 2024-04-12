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

        public static IEnumerator LoadAsyncScene(int sceneBuildIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
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
        
        // Kindly provided by ChakornK
        public static IEnumerator SmoothAlphaUpdate(Image targetGraphic, float targetAlpha, float duration) 
        {
            Color graphicColor = targetGraphic.color;
            float startTime = Time.deltaTime;
            float startAlpha = graphicColor.a;
            
            while (Time.deltaTime < startTime + duration) 
            {
                float t = (Time.deltaTime - startTime) / duration;
                targetGraphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, Mathf.Lerp(startAlpha, targetAlpha, t));
                yield return new WaitForEndOfFrame();
            }
        }
        public static IEnumerator SmoothAlphaUpdate(TextMeshProUGUI targetGraphic, float targetAlpha, float duration) 
        {
            Color graphicColor = targetGraphic.color;
            float startTime = Time.deltaTime;
            float startAlpha = graphicColor.a;
            
            while (Time.deltaTime < startTime + duration) 
            {
                float t = (Time.deltaTime - startTime) / duration;
                targetGraphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, Mathf.Lerp(startAlpha, targetAlpha, t));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
