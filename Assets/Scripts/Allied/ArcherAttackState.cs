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
        // TODO
        // ...

        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

        if (targets.GetLength(1) > 0)
        {
            // TODO
            rootFSM.gameObject.transform.LookAt(targets[0].transform.position);
        }
        rootFSM.StartCoroutine(AttackingTarget());
    }

    IEnumerator AttackingTarget()
    {
        yield return new WaitForSeconds(waitTime);

        if (false)
        {
            // TODO
            Debug.Log("Attacking");

            rootFSM.ChangeState(rootFSM.patrolState);
        }
        else
        {
            AttackTarget();
        }
    }
}
