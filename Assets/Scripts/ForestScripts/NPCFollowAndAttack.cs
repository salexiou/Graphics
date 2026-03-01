using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollowAndAttack : MonoBehaviour
{
    public AudioSource attackSound;
    public Transform player;
    public float followDistance = 1.5f; 
    public float attackDistance = 1.0f; 
    public float attackRate = 5.0f;
    public float attackDamage = 3/7f; 

    private NavMeshAgent agent;
    private Animator animator;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nextAttackTime = Time.time;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followDistance)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackDistance)
            {
                if(!AnimatorIsPlaying("Attack")){
                    animator.Play("Attack");
                    AttackPlayer();
                }
            }
            else
            {
                 if(!AnimatorIsPlaying("Walk")){
                    animator.Play("Walk");
                }
            }
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                attackSound.Play();
            }
        }

            nextAttackTime = Time.time + 1f / attackRate;
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
