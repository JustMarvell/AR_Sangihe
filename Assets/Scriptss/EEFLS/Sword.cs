using UnityEngine;

public class Sword : WeaponBase
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (currentCollider != null)
                oldCollider = currentCollider;
            currentCollider = other;

            if (currentCollider != oldCollider)
            {
                currentCollider.gameObject.SendMessage("TakeDamage", weaponStats.baseDamage);
                currentCollider.gameObject.SendMessage("ChangeState", EnemyNavigation.STATE.HIT);
            }
        }
    }

    void OnTriggerExit()
    {
        currentCollider = null;
        oldCollider = null;
    }
}