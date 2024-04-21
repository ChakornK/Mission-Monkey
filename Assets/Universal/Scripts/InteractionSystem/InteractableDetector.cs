using LemonStudios.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractableDetector : MonoBehaviour
{
    private Camera mainCamera;
    private Image interactionUI;
    private TextMeshProUGUI interactText;
    private PlayerInput playerInput;
    private Ray interactionRaycast;
    
    private LayerMask interactableMask;
    public float interactRayDistance = 2.5f;

    private void Start()
    {
        var interactionUIBase = GameObject.FindGameObjectWithTag("InteractUI");
        // Bit-shifting, my favorite!
        interactableMask = 1 << 6;
        
        mainCamera = GetComponentInChildren<Camera>();
        interactionUI = interactionUIBase.GetComponent<Image>();
        interactText = interactionUIBase.GetComponentInChildren<TextMeshProUGUI>();
        
        playerInput = new PlayerInput();
        playerInput.OnFoot.Interact.performed += FindInteractables;
        playerInput.Enable();
    }
    
    private void Update()
    {
        interactionRaycast = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        
        if (Physics.Raycast(interactionRaycast, out RaycastHit hit, interactRayDistance, interactableMask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                // Get the interactText string variable of the hit Interactable component and set it as the interact UI's text
                interactText.text = hit.collider.GetComponent<Interactable>().interactText;
                StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(interactionUI, 0.85f, 0.15f));
                StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(interactText, 1, 0.15f));
            }
        }
        // Disable Interaction UI the raycast doesn't detect any interactables
        else
        {
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(interactionUI, 0, 0.15f));
            StartCoroutine(LemonUIUtils.SmoothAlphaUpdate(interactText, 0, 0.15f));
        }
    }

    private void FindInteractables(InputAction.CallbackContext ctx)
    {
        if (Physics.Raycast(interactionRaycast, out RaycastHit hit, interactRayDistance, interactableMask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                // Debug.Log("Performing interact with interactable GameObject");
                hit.collider.GetComponent<Interactable>().TriggerInteract();
            }
        }
    }

    private void OnDestroy()
    {
        playerInput.Disable();
    }
}
