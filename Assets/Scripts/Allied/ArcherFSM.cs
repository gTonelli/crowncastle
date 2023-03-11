using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherFSM : MonoBehaviour
{
    private ArcherBaseState currentState;
    public ArcherBaseState CurrentState
    {
        get { return currentState; }
    }

    public ArcherPatrolState patrolState = new ArcherPatrolState();
    public ArcherAttackState attackState = new ArcherAttackState();
    public GameObject archerGameObject;
    public GameObject[] targets;
    public float moveSpeed;
    public Animator animator;
    public float waitTime;
    public float attackTriggerDistance;

    public static float arrowReleaseTime = 3.05f;

    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        archerGameObject = this.gameObject;
        animator = GetComponent<Animator>();

        ChangeState(patrolState);
    }

    public void ChangeState(ArcherBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
