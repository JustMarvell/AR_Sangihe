using System.Collections;
using UnityEngine;

public class ARPlayerAttackHandler : MonoBehaviour
{
    public PlayerStats playerStats;
    // TODO : add weapon stats to add to the player attack damage value

    public bool canAttack;
    public Animator animator;

    public void TriggerSwordAttack()
    {
        if (canAttack)
        {    
            canAttack = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(playerStats.attackCooldown);
        canAttack = true;
    }
}
