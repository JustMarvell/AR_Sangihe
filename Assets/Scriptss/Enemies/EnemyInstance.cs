using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    public EnemyStats enemyStat;
    public int currentHealth;

    public bool canAttack = false;
    public Transform detectionTransform;
    public float detectionRadius;
    public LayerMask targetLayer;


    void Start()
    {
        currentHealth = enemyStat.maxHealth;
    }

    public void ApplyDamage()
    {
        if (Physics.CheckSphere(detectionTransform.position, detectionRadius, targetLayer))
        {
            Debug.Log("Hit a player and apply " + enemyStat.baseDamage + " amount of damage");
            PlayerManager.instance.playerStats.TakeDamage(enemyStat.baseDamage);
        }
        else
        {
            Debug.Log("No player is hit");
        }
    }

    public void DestroyEnemy()
    {
        EnemyManager.instance.enemyList.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Taken " + damage + " amount of damage");

        if (currentHealth <= 0)
            KillEnemy();
    }

    void KillEnemy()
    {
        Debug.Log("Enemy Died");
        DestroyEnemy();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (detectionTransform != null)
            Gizmos.DrawWireSphere(detectionTransform.position, detectionRadius);
    }
}