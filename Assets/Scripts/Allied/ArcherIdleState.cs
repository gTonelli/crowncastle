using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherBaseState
{
    private int waitIntervals;

    public override void EnterState(ArcherFSM archerFSM)
    {
        base.EnterState(archerFSM);
        Idle();
    }

    private void Idle()
    {
        // TODO
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
        rootFSM.StartCoroutine(Idling());
    }

    IEnumerator Idling()
    {
        yield return new WaitForSeconds(waitTime * 8);

        Collider[] objectsInRange = Physics.OverlapSphere(rootFSM.archerGameObject.transform.position, attackTriggerDistance);

        foreach (Collider obj in objectsInRange)
        {
            if (obj.CompareTag("Skeleton"))
            {
                rootFSM.target = obj.gameObject;
                rootFSM.ChangeState(rootFSM.attackState);
                yield break;
            }
        }

        if (canPatrol) rootFSM.ChangeState(rootFSM.patrolState);
        else rootFSM.ChangeState(rootFSM.idleState);
    }
}
