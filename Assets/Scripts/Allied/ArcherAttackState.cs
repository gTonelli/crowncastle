using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : ArcherBaseState
{
    public override void EnterState(ArcherFSM archerFSM)
    {
        base.EnterState(archerFSM);
        Debug.Log("Archer Entered AttackState");
        AttackTarget();
    }

    private void AttackTarget()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        rootFSM.StartCoroutine(AttackingTarget());
    }

    IEnumerator AttackingTarget()
    {
        yield return new WaitForSeconds(waitTime);
        if (Vector3.Distance(target.transform.position, rootFSM.archerGameObject.transform.position) > attackTriggerDistance)
        {
            target = null;
            rootFSM.ChangeState(rootFSM.patrolState);
        }
        else
        {
            AttackTarget();
        }
    }
}
