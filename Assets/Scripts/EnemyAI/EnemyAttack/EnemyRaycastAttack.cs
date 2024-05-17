using UnityEngine;

public class EnemyRaycastAttack : EnemyAttacksBase
{
    protected override void enemyAttack()
    {
        base.enemyAttack();
        Vector3 raycastOrigin = firePoint.transform.position;
        Vector3 directionToTarget = (player.transform.position - raycastOrigin).normalized;
        if (enableDebug) Debug.DrawRay(raycastOrigin, directionToTarget);
        
        if(Physics.Raycast(raycastOrigin, directionToTarget, out RaycastHit hit))
        { 
            enemyHitPlayerActions(hit.collider.GetComponent<PlayerHealth>());
        }
    }
}
