using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    public ParticleSystem gunshotParticleSystem;
    public AudioClip patrolSoundEffect, targetFoundSoundEffect, playerNavigationEffect, attackSoundEffect;
    private AudioSource enemyAudioSource;


    private void Start()
    {
        try
        {
            enemyAudioSource = GetComponent<AudioSource>();
        }
        catch (MissingComponentException e)
        {
            Debug.LogWarning($"{gameObject.name} (Does not have an Audio source attached to the GameObject! \n{e}");
        }
    }

    public void playIdleSfx(bool playerInSight)
    {
        enemyAudioSource.PlayOneShot(playerInSight ? playerNavigationEffect : patrolSoundEffect);
    }

    public void playPlayerFound()
    {
        enemyAudioSource.PlayOneShot(targetFoundSoundEffect);
    }

    public void playAttackSfx()
    {
        enemyAudioSource.PlayOneShot(attackSoundEffect);
    }
    
}
