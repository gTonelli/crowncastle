using System.Collections;
using UnityEngine;

public class SkeletonMoveState : SkeletonBaseState
{
    public override void EnterState(SkeletonFSM skeletonFSM)
    {
        base.EnterState(skeletonFSM);
        agent.speed = moveSpeed;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
        rootFSM.StartCoroutine(MovingToTarget());
    }

    IEnumerator MovingToTarget()
    {
        yield return new WaitForSeconds(waitTime);

        if (Vector3.Distance(rootFSM.skeletonGameObject.transform.position, target.transform.position) < attackTriggerDistance)
        {
            rootFSM.ChangeState(rootFSM.attackState);
        }
        else
        {
            MoveToTarget();
        }
    }
}
