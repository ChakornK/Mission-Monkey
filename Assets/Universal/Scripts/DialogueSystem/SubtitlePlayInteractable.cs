using System;
using UnityEngine;

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
