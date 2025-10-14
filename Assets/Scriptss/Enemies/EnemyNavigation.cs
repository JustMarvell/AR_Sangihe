using NUnit.Framework;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class EnemyNavigation : MonoBehaviour
{
    #region old
    // NavMeshAgent agent;
    // Transform target;
    // EnemyInstance enemyInstance;

    // void Start()
    // {
    //     agent = GetComponent<NavMeshAgent>();

    //     agent.speed = GetComponent<EnemyInstance>().enemyStat.speed;
    //     target = PlayerManager.instance.gameObject.GetComponent<XROrigin>().Camera.transform;
    //     enemyInstance = GetComponent<EnemyInstance>();
    // }

    // void Update()
    // {
    //     if (target != null)
    //         agent.SetDestination(target.position);

    //     if (agent.remainingDistance < agent.stoppingDistance + .5f && agent.path.status == NavMeshPathStatus.PathComplete)
    //     {
    //         enemyInstance?.DetectTarget();
    //     }
    // }
    #endregion

    NavMeshAgent agent;
    Animator anim;
    EnemyInstance enemyInstance;

    public Transform player;
    public Collider weaponCollider;
    public enum STATE
    {
        IDLE,
        CHASE,
        MELLEATTACK,
        HIT
    }

    public STATE currentState = STATE.IDLE;

    private float waitTimer = 0f;
    private float attackTime = 1f;

    public float meleeDist = 1.5f;
    public bool isAttacking = false;
    private bool isInvincible = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyInstance = GetComponent<EnemyInstance>();
    }

    void Start()
    {
        player = PlayerManager.instance.gameObject.GetComponent<XROrigin>().Camera.transform;
    }

    public void EnableWeaponCollider()
    {
        if (isAttacking)
            weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

    void Update()
    {
        switch (currentState)
        {
            case STATE.IDLE:
                if (player != null)
                {
                    ChangeState(STATE.CHASE);
                }
                break;

            case STATE.CHASE:
                agent.SetDestination(player.position);
                if (agent.hasPath)
                {
                    if (CanAttackPlayer())
                    {
                        ChangeState(STATE.MELLEATTACK);
                    }
                }
                break;

            case STATE.MELLEATTACK:
                LookToTarget(2.0f);

                waitTimer += Time.deltaTime;
                if (waitTimer >= attackTime)
                {
                    if (CanAttackPlayer())
                    {
                        ChangeState(STATE.CHASE);
                    }
                    else
                    {
                        ChangeState(STATE.IDLE);
                    }
                }
                break;

            case STATE.HIT:
                waitTimer += Time.deltaTime;
                if (waitTimer < .5f)
                    LookToTarget(5f);
                else if (waitTimer >= .5f && isInvincible)
                    isInvincible = false;
                else if (waitTimer >= 1.25f)
                {
                    if (CanAttackPlayer())
                        ChangeState(STATE.CHASE);
                    else
                        ChangeState(STATE.IDLE);
                }
                break;
        }

        anim.SetFloat("speed", agent.velocity.magnitude);
    }

    private bool CanAttackPlayer()
    {
        Vector3 direction = player.position - transform.position;
        if (direction.magnitude < meleeDist)
            return true;
        return false;
    }

    public void ChangeState(STATE newState)
    {
        switch (currentState)
        {
            case STATE.IDLE:
                break;
            case STATE.CHASE:
                break;
            case STATE.MELLEATTACK:
                anim.ResetTrigger("attack");
                isAttacking = false;
                DisableWeaponCollider();
                break;
            case STATE.HIT:
                anim.ResetTrigger("hit");
                break;
        }
        switch (newState)
        {
            case STATE.IDLE:
                break;
            case STATE.CHASE:
                agent.speed = enemyInstance.enemyStat.speed;
                agent.isStopped = false;
                break;
            case STATE.MELLEATTACK:
                anim.SetTrigger("attack");
                agent.isStopped = true;
                waitTimer = 0;
                isAttacking = true;
                break;
            case STATE.HIT:
                anim.SetTrigger("hit");
                agent.isStopped = true;
                waitTimer = 0;
                break;
        }
        currentState = newState;
    }
    
    void LookToTarget(float speedRot)
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        direction.y = 0;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speedRot);
    }
}
