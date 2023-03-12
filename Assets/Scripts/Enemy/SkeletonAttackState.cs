using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : SkeletonBaseState
{
    public override void EnterState(SkeletonFSM skeletonFSM)
    {
        base.EnterState(skeletonFSM);
        AttackTarget();
    }

    private void AttackTarget()
    {
        if (agent) agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

        if (target)
        {
            rootFSM.gameObject.transform.LookAt(target.transform.position);
            rootFSM.StartCoroutine(AttackingTarget());
        }
        else
        {
            rootFSM.ChangeState(rootFSM.moveState);
        }
    }

    IEnumerator AttackingTarget()
    {
        yield return new WaitForSeconds(waitTime);

        if (target == null || Vector3.Distance(rootFSM.skeletonGameObject.transform.position, target.transform.position) > attackTriggerDistance)
        {
            rootFSM.ChangeState(rootFSM.moveState);
        }
        else
        {
            AttackTarget();
        }
    }
}
