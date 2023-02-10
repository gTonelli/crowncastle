using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonBaseState
{
    public override void EnterState(SkeletonFSM skeletonFSM)
    {
        base.EnterState(skeletonFSM);
        Debug.Log("Entered MoveState");

        agent.speed = moveSpeed;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        animator.SetBool("isWalking", true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
