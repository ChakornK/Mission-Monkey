using UnityEngine;

// As mentioned in DialogueContainer, a DialogueContainer component must be present in a script that needs to use it (crazy, I know)
[RequireComponent(typeof(DialogueContainer))]
public class PlayDialogueOnCollision : MonoBehaviour
{
    private DialogueContainer dialogueContainer;

    private void Start()
    {
        dialogueContainer = GetComponent<DialogueContainer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Start the coroutine inside the dialogueContainer component
        StartCoroutine(dialogueContainer.playDialogues());
    }
}
