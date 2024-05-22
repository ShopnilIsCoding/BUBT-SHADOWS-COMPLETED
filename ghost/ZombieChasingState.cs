using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChasingState : StateMachineBehaviour
{
   NavMeshAgent agent;
    Transform player;
    public float stopChasingDistance = 21;
    public float attackingDistance = 2.5f;
    public float chaseSpeed = 6f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    // Set the destination to the player's position
    agent.SetDestination(player.position);
    
    // Calculate the distance from the agent to the player
    float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
    
    // Check if the agent is within attacking distance
    if (distanceFromPlayer <= attackingDistance)
    {
        // If so, set attacking to true to transition to attacking state
        animator.SetBool("isAttacking", true);
    }
    
    // Check if the agent should stop chasing
    else if (distanceFromPlayer > stopChasingDistance)
    {
        animator.SetBool("isChasing", false);
    }
}


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerindex)
    {
        agent.SetDestination(animator.transform.position);
    }
}
