using UnityEngine;

[CreateAssetMenu(fileName ="New player stat", menuName ="Player/Create Stat")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public int baseDamage { get; private set; }
    public float attackCooldown { get; private set; }

    public void InitializePlayerStats()
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
        else
            Debug.Log("health is less than zero");
        if (currentHealth < 0)
            currentHealth = 0;
    }
}