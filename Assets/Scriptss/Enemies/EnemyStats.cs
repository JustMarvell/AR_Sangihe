using UnityEngine;

[CreateAssetMenu(fileName="Enemy Stats", menuName="Enemy/Create Stats")]
public class EnemyStats : ScriptableObject
{
    public int maxHealth = 100;

    public int baseDamage = 5;
    public float speed = 3f;
}