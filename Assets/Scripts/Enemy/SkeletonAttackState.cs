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
        try
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
            rootFSM.gameObject.transform.LookAt(target.transform.position);
            rootFSM.StartCoroutine(AttackingTarget());
        }
        catch (System.Exception)
        {
            Debug.Log("Skeleotn was deleted");
            throw;
        }
    }

    IEnumerator AttackingTarget()
    {
        yield return new WaitForSeconds(waitTime);

        if (Vector3.Distance(rootFSM.skeletonGameObject.transform.position, target.transform.position) > attackTriggerDistance)
        {
            Debug.Log("Attacking");

            rootFSM.ChangeState(rootFSM.moveState);
        }
        else
        {
            AttackTarget();
        }
    }
}
