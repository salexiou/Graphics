using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCsBehavior : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        agent.SetDestination(player.position);

        if(agent.speed != 0){
            if(!AnimatorIsPlaying("Walk")){
                    animator.Play("Walk");
                }           
        } else {
                if(!AnimatorIsPlaying("Idle")){
                    animator.Play("Idle");
                }
        }
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

