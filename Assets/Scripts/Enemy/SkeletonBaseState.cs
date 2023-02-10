using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class SkeletonBaseState
{
    protected float distanceToTarget;
    protected NavMeshAgent agent;
    protected Transform target;

    protected float attackTriggerDistance;
    protected float chaseTriggerDistance;

    protected SkeletonFSM skeletonFSM;
    protected Animator animator;

    protected float moveSpeed;
    protected float chaseSpeed;

    public virtual void EnterState(SkeletonFSM skeletonFSM)
    {
        target = skeletonFSM.target;
        agent = skeletonFSM.agent;
        moveSpeed = skeletonFSM.moveSpeed;
        animator = skeletonFSM.animator;

    }
}
