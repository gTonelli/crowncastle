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
        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            if (target == null)
            {
                rootFSM.target = GameObject.Find("Player");
                target = GameObject.Find("Player");
                Debug.Log("Player was assigned as target");
            }
            agent.SetDestination(target.transform.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
            rootFSM.StartCoroutine(MovingToTarget());
        }

    }

    IEnumerator MovingToTarget()
    {
        yield return new WaitForSeconds(waitTime);
        Collider[] objectsInRange = Physics.OverlapSphere(rootFSM.skeletonGameObject.transform.position, attackTriggerDistance * 5);

        GameObject closestTarget = null;

        foreach (Collider obj in objectsInRange)
        {
            if (obj.CompareTag("Archer"))
            {
                if (closestTarget == null || GetTargetDistance(obj.gameObject) < GetTargetDistance(closestTarget))
                {
                    closestTarget = obj.gameObject;
                }
            }
        }

        if (closestTarget)
        {
            rootFSM.target = closestTarget;
            target = closestTarget;
        }

        if (rootFSM.target && GetTargetDistance(rootFSM.target) < attackTriggerDistance)
        {
            rootFSM.ChangeState(rootFSM.attackState);
        }
        else
        {
            MoveToTarget();
        }
    }

    private float GetTargetDistance(GameObject other)
    {
        return Vector3.Distance(rootFSM.skeletonGameObject.transform.position, other.transform.position);
    }
}
