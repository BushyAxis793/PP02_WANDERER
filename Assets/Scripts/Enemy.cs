using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] int health = 100;
    [SerializeField] Transform granadePosition;
    bool isAlive = true;

    [Header("Enemy Attack")]
    [SerializeField] Transform target;
    [SerializeField] int damage = 20;
    public float lookRadius = 10f;
    bool isChasing;
    bool isAttacking;

    [Header("Enemy Patrol")]
    [SerializeField] Transform[] patrolSpots;
    float speed;
    int randomSpot;
    float targetDistance;

    Player player;

    NavMeshAgent agent;

    Granade granade;

    Animator anim;

    private void Start()
    {
        isAttacking = false;
        isChasing = false;
        randomSpot = Random.Range(0, patrolSpots.Length);

        anim = GetComponent<Animator>();
        granade = FindObjectOfType<Granade>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        EnemyPatrol();
        EnemyAttack();
        
    }

    public void TakeDamageFromGranade()
    {
        if (isAlive)
        {
            float distanceToGranade = Vector3.Distance(transform.position, granadePosition.position);
            if (distanceToGranade < granade.explosionRadius)
            {
                anim.SetTrigger("GetHit");
                health -= 10;
                if (health <= 0)
                {
                    anim.SetTrigger("Dead");
                    anim.enabled = false;
                    Destroy(gameObject, 10f);
                }

            }
        }
    }
    public void TakeDamageFromWeapon(int damage)
    {
        Debug.Log("odejmuje zycie");
        //anim.SetTrigger("GetHit");
        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("Dead");
            anim.enabled = false;
            Destroy(gameObject, 10f);
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

        if (isChasing)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
        }
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

        if (isAttacking)
        {
            
            anim.SetTrigger("Attack");
            anim.SetBool("Run", false);
        }
    }


}
