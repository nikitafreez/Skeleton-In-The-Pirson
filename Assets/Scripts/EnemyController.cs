using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrol;
    public NavMeshAgent agent;

    public Animator anim;

    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking, isDying
    };
    public AIState currentState;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;

    public float attackRange = 2f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    private float animWait;

    public static EnemyController instance;
    private void Awake() {
        instance = this;
    }

    void Start()
    {
        waitCounter = waitAtPoint;
        animWait = 1.5f;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (EnemyHealthManager.instance.currentHealth <= 0)
        {
            if (animWait > 0)
            {
                animWait -= Time.deltaTime;
            }
            if (EnemyHealthManager.instance.currentHealth == 0)
            {
                anim.SetBool("IsMoving", false);
                anim.SetTrigger("Death");
            }
            if(animWait <= 0){
                EnemyHealthManager.instance.DestroyEnemy();
            }
            EnemyHealthManager.instance.currentHealth--;
        }
        if (EnemyHealthManager.instance.currentHealth > 0)
        {
            switch (currentState)
            {
                case AIState.isIdle:
                    anim.SetBool("IsMoving", false);

                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIState.isPatrolling;
                        agent.SetDestination(patrolPoints[currentPatrol].position);
                    }

                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                        anim.SetBool("IsMoving", true);
                    }

                    break;

                case AIState.isPatrolling:

                    if (agent.remainingDistance <= .2f)
                    {
                        currentPatrol++;
                        if (currentPatrol >= patrolPoints.Length)
                        {
                            currentPatrol = 0;
                        }

                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;
                    }

                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                    }

                    anim.SetBool("IsMoving", true);

                    break;

                case AIState.isChasing:

                    agent.SetDestination(PlayerController.instance.transform.position);

                    if (distanceToPlayer <= attackRange)
                    {
                        currentState = AIState.isAttacking;
                        anim.SetTrigger("Attack");
                        anim.SetBool("IsMoving", false);

                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;

                        attackCounter = timeBetweenAttacks;
                    }

                    if (distanceToPlayer > chaseRange)
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        agent.velocity = Vector3.zero;
                        agent.SetDestination(transform.position);
                    }

                    break;

                case AIState.isAttacking:

                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                    attackCounter -= Time.deltaTime;
                    if (attackCounter <= 0)
                    {
                        if (distanceToPlayer < attackRange)
                        {
                            anim.SetTrigger("Attack");
                            attackCounter = timeBetweenAttacks;
                        }
                        else
                        {
                            currentState = AIState.isIdle;
                            waitCounter = waitAtPoint;

                            agent.isStopped = false;
                        }
                    }
                    break;
            }
        }
    }
}
