using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherPatrolState : ArcherBaseState
{
    private float range = 8f;

    public override void EnterState(ArcherFSM archerFSM)
    {
        base.EnterState(archerFSM);
        agent.speed = moveSpeed;
        Patrol();
    }

    private void Patrol()
    {
        animator.SetBool("isAttacking", false);

        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            if (RandomPoint(rootFSM.archerGameObject.transform.position, range, out Vector3 point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
                animator.SetBool("isWalking", true);
                rootFSM.StartCoroutine(Patrolling());
            }
            else
            {
                rootFSM.ChangeState(rootFSM.idleState);
            }
        }
        else
        {
            rootFSM.StartCoroutine(Patrolling());
        }
    }

    IEnumerator Patrolling()
    {
        yield return new WaitForSeconds(waitTime);

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

        Patrol();
    }

    // Taken from https://github.com/JonDevTutorial/RandomNavMeshMovement/blob/main/RandomMovement.cs
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + (Random.insideUnitSphere * range); //random point in a sphere 
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
