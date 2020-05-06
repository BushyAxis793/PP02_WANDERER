using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy attack")]
    public float lookRadius = 10f;
    public Transform target;
    NavMeshAgent agent;
    private bool isChasing;
    private bool isAttacking;
    //Controller playerController;

    public float damage = 10f;


    [Header("Enemy patrol")]
    public float speed;
    public Transform[] patrolSpots;
    private int randomSpot;
    private float distance;

    //Animations
    //EnemyAnimController enemyAnimController;
    Animator anim;

   
    
    void Awake()
    {
        isChasing = false;

        agent = GetComponent<NavMeshAgent>();

        randomSpot = Random.Range(0, patrolSpots.Length);

        anim = GetComponent<Animator>();

        //playerController = GetComponent<Controller>();


    }

    void Update()
    {
        Patrol();
        Attack();
        
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Patrol()
    {
        isChasing = false;

        if (agent.remainingDistance < 0.5f)
        {
            if (patrolSpots.Length == 0)
            {
                return;
            }

            agent.destination = patrolSpots[randomSpot].position;

            randomSpot = (randomSpot + 1) % patrolSpots.Length;
        }

        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            isChasing = true;

            agent.SetDestination(target.position);

            LookAtTarget();



        }

        if (isChasing)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);

        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
        }

    }

    void Attack()
    {

        if (distance < 3f)
        {
            //TakeDamege();
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
        }
        else if (!isAttacking && isChasing)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);

        }
        else if (!isAttacking && !isChasing)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }

    }

    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }




}
