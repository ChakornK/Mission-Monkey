using UnityEngine;

public class DebugHealthInteractable : Interactable
{
    public int healthModifier;
    public bool damagePlayer = true; 
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    
    protected override void Interact()
    {
        base.Interact();
        playerHealth.ModifyPlayerHealth(healthModifier, damagePlayer);
    }
}
