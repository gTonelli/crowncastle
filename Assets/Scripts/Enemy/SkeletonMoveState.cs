using System.Collections;
using UnityEngine;

public class SkeletonMoveState : SkeletonBaseState
{
    public override void EnterState(SkeletonFSM skeletonFSM)
    {
        base.EnterState(skeletonFSM);
        Debug.Log("Entered MoveState");

        agent.speed = moveSpeed;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
        Debug.Log(target.transform.position);
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);

        rootFSM.StartCoroutine(MovingToTarget());
    }

    IEnumerator MovingToTarget()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log(waitTime);

        if (Vector3.Distance(rootFSM.skeletonGameObject.transform.position, target.transform.position) < attackTriggerDistance)
        {
            Debug.Log("Attacking");
            rootFSM.ChangeState(rootFSM.attackState);
        }
        else
        {
            MoveToTarget();
        }
    }
}
