using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapon/Create Weapon")]
public class WeaponStats : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public int baseDamage;
}