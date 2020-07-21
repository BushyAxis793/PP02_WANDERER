using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] int health = 100;
    bool isAlive = true;

    [Header("Enemy Attack")]
    [SerializeField] Transform target;
    [SerializeField] int damage = 20;
    public float lookRadius = 10f;
    bool isChasing;
    bool isAttacking;

    [Header("Enemy Patrol")]
    [SerializeField] Transform[] patrolSpots;
    int randomSpot;
    float targetDistance;

    [SerializeField] float speed;

    const string WALK_BOOL = "Walk";
    const string RUN_BOOL = "Run";
    const string GET_HIT_TRIGGER = "GetHit";
    const string DEAD_TRIGGER = "Dead";
    const string ATTACK_TRIGGER = "Attack";

    NavMeshAgent agent;
    Animator anim;

    private void Start()
    {
        isAttacking = false;
        isChasing = false;
        randomSpot = Random.Range(0, patrolSpots.Length);

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (isAlive)
        {
            EnemyPatrol();
            EnemyAttack();
        }
    }
    public void TakeDamageFromWeapon(int damage)
    {
        if (isAlive)
        {
            anim.SetTrigger(GET_HIT_TRIGGER);
            health -= damage;
            if (health <= 0)
            {
                isAlive = false;
                agent.Stop();
                anim.SetTrigger(DEAD_TRIGGER);
                Destroy(gameObject, 10f);
            }
        }
    }
    public void GiveDamage()
    {
        FindObjectOfType<Player>().TakeDamage(damage);
    }
    private void LookAtTarget()
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Quaternion lookTarget = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, Time.deltaTime * 4f);
    }
    private void EnemyPatrol()
    {
        isChasing = false;

        if (agent.remainingDistance < .5f)
        {
            if (patrolSpots.Length == 0)
            {
                return;
            }

            agent.destination = patrolSpots[randomSpot].position;

            randomSpot = (randomSpot + 1) % patrolSpots.Length;

        }

        targetDistance = Vector3.Distance(target.position, transform.position);

        if (targetDistance <= lookRadius)
        {
            isChasing = true;

            agent.SetDestination(target.position);

            LookAtTarget();
        }

        AnimationHandler();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    private void EnemyAttack()
    {
        if (targetDistance <= 3f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
    private void AnimationHandler()
    {
        if (isChasing)
        {
            anim.SetBool(RUN_BOOL, true);
            anim.SetBool(WALK_BOOL, false);
        }
        else
        {
            anim.SetBool(RUN_BOOL, false);
            anim.SetBool(WALK_BOOL, true);
        }
        if (isAttacking)
        {
            anim.SetTrigger(ATTACK_TRIGGER);
            anim.SetBool(RUN_BOOL, false);
        }
    }
}
