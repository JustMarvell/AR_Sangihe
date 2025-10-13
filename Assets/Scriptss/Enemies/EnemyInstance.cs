using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    public EnemyStats enemyStat;

    void Start()
    {
        enemyStat?.InitializeEnemyStat();
    }

    public void DestroyEnemy()
    {
        EnemyManager.instance.enemyList.Remove(this.gameObject);
        DestroyImmediate(gameObject);
    }

    public void TakeDamage(int damage)
    {
        enemyStat.TakeDamage(damage);
        if (enemyStat.currentHealth <= 0)
            KillEnemy();
    }

    void KillEnemy()
    {
        Debug.Log("Enemy Died");
        DestroyEnemy();
    }
}