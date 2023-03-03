using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPatrolState : ArcherBaseState
{
    public override void EnterState(ArcherFSM archerFSM)
    {
        base.EnterState(archerFSM);
        Debug.Log("Archer Entered PatrolState");

        agent.speed = moveSpeed;
        Patrol();
    }

    private void Patrol()
    {
        // TODO
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);

        rootFSM.StartCoroutine(Patrolling());
    }

    IEnumerator Patrolling()
    {
        yield return new WaitForSeconds(waitTime);

        Collider[] objectsInRange = Physics.OverlapSphere(rootFSM.gameObject.transform.position, attackTriggerDistance);

        foreach (Collider obj in objectsInRange)
        {
            if (obj.gameObject.tag == "Skeleton")
            {
                Debug.Log("Tag" + obj.gameObject.tag);
            }
        }

        if (false)
        {
            Debug.Log("Attacking");
            rootFSM.ChangeState(rootFSM.attackState);
        }
        else
        {
            Patrol();
        }
    }
}
