using System;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private GameObject player;
    
    public float projectileSpeed, projectileDestroyTime;
    public bool followPlayer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        if (followPlayer)
        {
            
        }
        
        // Fire a short raycast to determine if the projectile is about to hit something. if it does, it destroys itself
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, 0.25f))
        {
            if (hit.collider.gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}