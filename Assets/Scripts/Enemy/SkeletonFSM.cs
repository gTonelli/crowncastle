using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SkeletonFSM : MonoBehaviour
{

    private SkeletonBaseState currentState;
    public SkeletonBaseState CurrentState
    {
        get { return currentState; }
    }

    public SkeletonMoveState moveState = new SkeletonMoveState();
    public GameObject skeletonGameObject;
    public Transform target;
    public float moveSpeed;
    public Animator animator;

    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        skeletonGameObject = this.gameObject;
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player").GetComponent<Transform>();

        ChangeState(moveState);
    }

    public void ChangeState(SkeletonBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
