using System;
using System.Collections;
using UnityEngine;


public class DialogueContainer : MonoBehaviour
{
    public AudioClip[] dialogues;
    public string[] dialoguesTranscript;
    
    public AudioSource audioPlayLocation;
    public SubtitlesUI subtitlesUI;

    public IEnumerator playDialogues()
    {
        // Make the subtitles UI appear
        if (subtitlesEnabled()) subtitlesUI.ShowSubtitles();
        
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i] != null)
            {
                audioPlayLocation.PlayOneShot(dialogues[i]);
                if (subtitlesEnabled())
                {
                    subtitlesUI.UpdateSubtitles(dialoguesTranscript[i]);
                }
                yield return new WaitForSeconds(dialogues[i].length);
            }
            else
            {
                Debug.LogWarning(gameObject.name + " does not have a dialogue referenced at index " + i);
                yield return new WaitForEndOfFrame();
            }
        }
        
        if(subtitlesEnabled()) subtitlesUI.HideSubtitles();
    }

    private bool subtitlesEnabled()
    {
        return subtitlesUI.subtitlesEnabled;
    }
}
