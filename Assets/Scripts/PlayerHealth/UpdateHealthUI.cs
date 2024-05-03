using LemonStudios.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthUI : MonoBehaviour
{
    public GameObject healthUI;
    public TextMeshProUGUI healthText;
    public float healthbarUpdateSpeed;
    
    private Image playerHealthImage;
    private Color highHealthColour, midHealthColour, lowHealthColour;

    private void Start()
    {
        // I might want to make these values be more flexible in the future, them being hardcoded is fine for now
        highHealthColour = LemonUIUtils.CreateNormalizedColor(71, 210, 107);
        midHealthColour = LemonUIUtils.CreateNormalizedColor(241, 195, 57);
        lowHealthColour = LemonUIUtils.CreateNormalizedColor(202, 22, 34);

        playerHealthImage = healthUI.GetComponent<Image>();
    }

    public void OnHealthModification()
    {
        int currentPlayerHealth = GetComponent<PlayerHealth>().GetHealth();

        switch (currentPlayerHealth)
        {
            case >= 65:
                playerHealthImage.color = highHealthColour;
                break;
            case >= 35 and <= 64:
                playerHealthImage.color = midHealthColour;
                break;
            case >= 0 and <= 34:
                playerHealthImage.color = lowHealthColour;
                break;
        }

        healthText.text = currentPlayerHealth + "/100";

        StartCoroutine(LemonUIUtils.SmoothlyUpdateFillUI(playerHealthImage, (float)currentPlayerHealth / 100, healthbarUpdateSpeed));
    }
}
