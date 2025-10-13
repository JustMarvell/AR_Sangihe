using UnityEngine;

[CreateAssetMenu(fileName="Enemy Stats", menuName="Enemy/Create Stats")]
public class EnemyStats : ScriptableObject
{
    public int maxHealth = 100;
    public int currentHealth = 0;

    public int baseDamage = 5;

    public void InitializeEnemyStat()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log("Taken " + damage + " amount of damage");
        }
        if (currentHealth < 0)
            currentHealth = 0;
    }
}