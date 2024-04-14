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
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i] != null)
            {
                if (subtitlesUI.areSubtitlesHidden())
                {
                    subtitlesUI.ShowSubtitles(dialoguesTranscript[i]);
                }
                else
                {
                    subtitlesUI.UpdateSubtitles(dialoguesTranscript[i]);
                }
                
                audioPlayLocation.PlayOneShot(dialogues[i]);
                yield return new WaitForSeconds(dialogues[i].length);
            }
            else
            {
                Debug.LogWarning(gameObject.name + " does not have a dialogue referenced at index " + i);
                yield return new WaitForEndOfFrame();
            }
        }
        subtitlesUI.HideSubtitles();
    }

    private bool subtitlesEnabled()
    {
        return subtitlesUI.subtitlesEnabled;
    }
}
