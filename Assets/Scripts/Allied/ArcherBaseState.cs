using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherBaseState : MonoBehaviour
{
    protected float distanceToTarget;
    protected NavMeshAgent agent;
    protected GameObject[] targets;

    protected float attackTriggerDistance;
    protected float chaseTriggerDistance;

    protected ArcherFSM rootFSM;
    protected Animator animator;

    protected float moveSpeed;

    protected float waitTime;

    public virtual void EnterState(ArcherFSM archerFSM)
    {
        targets = archerFSM.targets;
        agent = archerFSM.agent;
        moveSpeed = archerFSM.moveSpeed;
        animator = archerFSM.animator;
        waitTime = archerFSM.waitTime;
        attackTriggerDistance = archerFSM.attackTriggerDistance;
        rootFSM = archerFSM;
    }
}
