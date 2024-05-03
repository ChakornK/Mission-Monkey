using TMPro;
using UnityEngine;

public class SetNewVersionInfo : MonoBehaviour
{
    public TextMeshProUGUI newVersionTitle;
    
    private void Start()
    {
        newVersionTitle.text = "Welcome To " + Application.version + "!";
    }
}
