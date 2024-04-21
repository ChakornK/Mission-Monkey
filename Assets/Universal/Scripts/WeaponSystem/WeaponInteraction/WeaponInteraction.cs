using UnityEngine;

public abstract class WeaponInteraction : MonoBehaviour
{
    public bool enableDebugMessages;
    protected virtual void OnWeaponInteract()
    {
        if(enableDebugMessages) Debug.Log($"hit a weapon interactable: {gameObject.name}!");
    }

    public void PerformWeaponInteraction()
    {
        // Needed for the weapon system to interact with the weapon interact system (It works so i don't care)
        OnWeaponInteract();
    }
}
