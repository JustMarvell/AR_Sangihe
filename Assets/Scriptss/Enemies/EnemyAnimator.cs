using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public Collider weaponCollider;
    public bool isAttacking = false;

    public string speedParameter = "speed";
    public string attackParameter = "attack";

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat(speedParameter, agent.velocity.magnitude);
    }
}
