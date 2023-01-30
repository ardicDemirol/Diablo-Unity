using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaypointTracker1 : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] walkPoints;
    [Header("Movement Settings")]
    public float turnSpeed = 5f;
    public float patrolTime = 10f;
    public float walkDistance = 8f;
    [Header("Attack Settings")]
    public float attackDistance = 1.4f;
    public float attackRate = 1f;

    private Transform playerTarget;
    private Animator anim;

    private NavMeshAgent agent;

    private float currentAttackTime;

    private Vector3 nextDestination;
    private int index;

    //health

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        index = Random.Range(0,walkPoints.Length);
        if(walkPoints.Length > 0)
        {
            InvokeRepeating(nameof(Patrol),Random.Range(0,patrolTime),patrolTime);
        }
    }


    void Start()
    {
        agent.avoidancePriority = Random.Range(1,51);
    }

    void Update()
    {
        MoveAndAttack();
    }

    void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position,playerTarget.position);
        if(distance > walkDistance)
        {
            if(agent.remainingDistance >= agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.speed = 2f;
                anim.SetBool("Walk",true);

                nextDestination = walkPoints[index].position;
                agent.SetDestination(nextDestination);
            }
            else
            {
                agent.isStopped = true;
                agent.speed = 0f;
                anim.SetBool("Walk", false);

                nextDestination = walkPoints[index].position;
                agent.SetDestination(nextDestination);

            }
        }
        else
        {
            if (distance >= attackDistance + 0.15f && playerTarget.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                if(!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    anim.ResetTrigger("Attack");
                    agent.isStopped = false;
                    agent.speed = 3f;
                    anim.SetBool("Walk", true);

                    agent.SetDestination(playerTarget.position);
                }
            }
            else if(distance <= attackDistance && playerTarget.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                agent.isStopped = true;
                anim.SetBool("Walk", false);
                agent.speed = 0f;
                Vector3 targetPos = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position),turnSpeed * Time.deltaTime);
                if(currentAttackTime >= attackRate)
                {
                    anim.SetTrigger("Attack");
                    currentAttackTime = 0f;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                }
            }
        }
    }
    void Patrol()
    {
        index = index == walkPoints.Length - 1 ? 0 : index + 1;
    }
}
