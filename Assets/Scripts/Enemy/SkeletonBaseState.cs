using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class SkeletonBaseState
{
    protected float distanceToTarget;
    protected NavMeshAgent agent;
    protected GameObject target;

    protected float attackTriggerDistance;
    protected float chaseTriggerDistance;

    protected SkeletonFSM rootFSM;
    protected Animator animator;

    protected float moveSpeed;
    protected float chaseSpeed;

    protected float waitTime;

    public virtual void EnterState(SkeletonFSM skeletonFSM)
    {
        target = skeletonFSM.target;
        agent = skeletonFSM.agent;
        moveSpeed = skeletonFSM.moveSpeed;
        animator = skeletonFSM.animator;
        waitTime = skeletonFSM.waitTime;
        attackTriggerDistance = skeletonFSM.attackTriggerDistance;
        rootFSM = skeletonFSM;
    }
}
