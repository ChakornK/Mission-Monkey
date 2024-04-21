using System.Collections;
using UnityEngine;


// Used with some form of dialogue player script. any one of the dialogue player scripts will require one of these components on the GameObject that they are attached to,
// So they will be auto-added
public class DialogueContainer : MonoBehaviour
{
    public AudioClip[] dialogues;
   
    // Probably not the best way to handle subtitle reading, but it sure does work!
    public string[] dialoguesTranscript;
    
    // For now, all audio will play on the player's AudioPlayer, but in the future that won't be the case and the audio will play from elsewhere
    // Just a little more complicated for my knowledge (Still a learning C# developer after all)
    public AudioSource audioPlayLocation;
    public SubtitlesUI subtitlesUI;

    
    // Coroutines are the best!
    public IEnumerator playDialogues()
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i] != null)
            {
                if (subtitlesUI.areSubtitlesHidden())
                {
                    // Checking if subtitles are enabled are done in the SubtitlesUI class
                    subtitlesUI.ShowSubtitles(dialoguesTranscript[i]);
                }
                else
                {
                    subtitlesUI.UpdateSubtitles(dialoguesTranscript[i]);
                }
                audioPlayLocation.PlayOneShot(dialogues[i]);
                
                // Wait for the length of the AudioClip that just played before playing the next one
                yield return new WaitForSeconds(dialogues[i].length);
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} does not have a dialogue referenced at index {i}");
                // If no audio is found at the current index, attempt to play the next entry in the array on the next frame
                yield return new WaitForEndOfFrame();
            }
        }
        subtitlesUI.HideSubtitles();
    }
}
