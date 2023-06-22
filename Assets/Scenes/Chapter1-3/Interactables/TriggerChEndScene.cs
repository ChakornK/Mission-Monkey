using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
public class TriggerChEndScene : Interactable
{
    public GameObject InterctObj, EscapePod;
    protected override void Interact()
    {
        TiePlayerToEscPod.InEscapePod = true;
        EscapePod.GetComponent<Animator>().SetBool("MonkeyInEscapePod", true);
        Destroy(InterctObj);
    }
}
