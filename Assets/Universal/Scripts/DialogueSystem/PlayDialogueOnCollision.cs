using UnityEngine;

public class PlayDialogueOnCollision : MonoBehaviour
{
    private DialogueContainer dialogueContainer;

    private void Start()
    {
        dialogueContainer = GetComponent<DialogueContainer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(dialogueContainer.playDialogues());
    }
}
