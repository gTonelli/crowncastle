using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ArcherBaseState
{
    protected float distanceToTarget;
    protected NavMeshAgent agent;
    protected GameObject target;

    protected float attackTriggerDistance;
    protected float chaseTriggerDistance;

    protected ArcherFSM rootFSM;
    protected Animator animator;

    protected float moveSpeed;

    protected float waitTime;

    public virtual void EnterState(ArcherFSM archerFSM)
    {
        target = archerFSM.target;
        agent = archerFSM.agent;
        moveSpeed = archerFSM.moveSpeed;
        animator = archerFSM.animator;
        waitTime = archerFSM.waitTime;
        attackTriggerDistance = archerFSM.attackTriggerDistance;
        rootFSM = archerFSM;
    }
}
