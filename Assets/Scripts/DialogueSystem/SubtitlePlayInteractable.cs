using UnityEngine;

// As mentioned in DialogueContainer, a DialogueContainer component must be present in a script that needs to use it (crazy, I know)
[RequireComponent((typeof(DialogueContainer)))]
public class SubtitlePlayInteractable : Interactable
{
    private DialogueContainer dialogueContainer;

    private void Start()
    {
        dialogueContainer = GetComponent<DialogueContainer>();
    }

    protected override void Interact()
    {
        StartCoroutine(dialogueContainer.playDialogues());
        base.Interact();
    }
}
