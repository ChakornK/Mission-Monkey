using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdTimeThreshold = 1.0f; // Set the time threshold for holding the button
    public PlayerMotor playerMotor;
    
    public CameraMove cameraMove;

    void Awake()
    {
        cameraMove = FindFirstObjectByType<CameraMove>();
    }
    void Start()
    {

    }

    void Update()
    {
        // Check if the button is being held

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        
        cameraMove.ignoreTouch++;
        playerMotor.Jump();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
       
        cameraMove.ignoreTouch--;
    }





   
}
