using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadOnCollision : MonoBehaviour
{
    public string sceneToLoad;
    public bool enableDebugMessage;
    public void OnTriggerEnter(Collider other)
    {
        if (enableDebugMessage) Debug.Log("load scene");
        
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}