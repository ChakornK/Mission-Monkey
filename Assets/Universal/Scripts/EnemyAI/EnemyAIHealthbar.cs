using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIHealthbar : MonoBehaviour
{
    public Image enemyHealthbarTop, enemyHealthbarMiddle;
    public TextMeshProUGUI healthValue, enemyName;
    public float healthbarUpdateSpeed = 15.5f;
    public float middleHalthbarUpdateSpeed = 25.5f;
    public GameObject enemyHealthbar;

    private void Update()
    {
        // Healthbar shouldn't be active if the enemy is dead
        if(enemyHealthbarTop.fillAmount <= 0.01f)
        {
            enemyHealthbar.SetActive(false);
        }
    }

    public void SetEnemyHealthbar(EnemyAIHealth enemy)
    {
        // This can only trigger if the player hasn't attacked an enemy in the 
        // current play session or if they killed the previous enemy.
        if(!enemyHealthbar.activeSelf)
        {
            enemyHealthbar.SetActive(true);
        }
        if(enemy.GetAIHealth() == 0)
        {
            enemyHealthbar.SetActive(false);
            return;
        }

        enemyName.text = enemy.GetAIName();
        healthValue.text = enemy.GetAIHealth() + " / " + enemy.GetMaxAIHealth();

        // Pretty much the same thing as the one in UpdateHealthUI
        var lerpValue = enemy.GetAIHealth() / enemy.GetMaxAIHealth();
        StartCoroutine(LerpEnemyImageFillValue(lerpValue, enemyHealthbarTop, healthbarUpdateSpeed));
        StartCoroutine(LerpEnemyImageFillValue(lerpValue, enemyHealthbarMiddle, middleHalthbarUpdateSpeed));
    }

    private IEnumerator LerpEnemyImageFillValue(float targetFill, Image targetGraphic, float targetGraphicUpdateSpeed)
    {
        float currentFillAmount = targetGraphic.fillAmount;
        
        // While the current fill of the Image is not the target fill, lerp the value towards the desired value each frame
        while(Mathf.Abs(currentFillAmount - targetFill) > 0.01f)
        {
            currentFillAmount = Mathf.Lerp(currentFillAmount, targetFill, Time.deltaTime * targetGraphicUpdateSpeed);
            enemyHealthbarTop.fillAmount = currentFillAmount;
            yield return new WaitForEndOfFrame();
        }
    }
}
