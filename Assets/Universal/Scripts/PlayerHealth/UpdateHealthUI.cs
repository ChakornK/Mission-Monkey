using LemonStudios.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthUI : MonoBehaviour
{
    public GameObject healthUI;
    public TextMeshProUGUI healthText;
    public float healthBarLerpSpeed = 10;
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

    public void Update()
    {
        int currentPlayerHealth = GetComponent<PlayerHealth>().GetHealth();

        if (currentPlayerHealth >= 65)
        {
            playerHealthImage.color = highHealthColour;
        }

        if (currentPlayerHealth is >= 35 and <= 64)
        {
            playerHealthImage.color = midHealthColour;
        }

        if (currentPlayerHealth is >= 0 and <= 34)
        {
            playerHealthImage.color = lowHealthColour;
        }

        healthText.text = currentPlayerHealth.ToString() + "/100";

        float currentFillAmount = playerHealthImage.fillAmount;
        float fillAmount = Mathf.Lerp(currentFillAmount, currentPlayerHealth / 100f, Time.deltaTime * healthBarLerpSpeed);
        playerHealthImage.fillAmount = fillAmount;
    }
}
