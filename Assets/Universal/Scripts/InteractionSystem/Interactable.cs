using UnityEngine;

// This interaction system is very similar to the old one, but I just want to have the experience of not using a tutorial to write it
public abstract class Interactable : MonoBehaviour
{
    public bool enableDebugLogging;
    public string interactText;
    
    // Specifically for InteractableDetector so it can trigger the protected Interact() method (probably a bad way to do this, but if I find a better way I'll come back and change the code to reflect on my newfound knowledge) 
    public void TriggerInteract()
    {
        Interact();
    }
    
    protected virtual void Interact()
    {
        // Completely empty (aside from debug logging), as other scripts will be overriding this script to have their own functionality
        if(enableDebugLogging) Debug.Log($"Interacted with {gameObject.name}");
    }
}
