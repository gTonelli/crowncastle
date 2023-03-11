using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherFSM : MonoBehaviour
{
    private ArcherBaseState currentState;
    public ArcherBaseState CurrentState
    {
        get { return currentState; }
    }

    public ArcherPatrolState patrolState = new ArcherPatrolState();
    public ArcherAttackState attackState = new ArcherAttackState();
    public GameObject archerGameObject;
    public GameObject[] targets;
    public float moveSpeed;
    public Animator animator;
    public float waitTime;
    public float attackTriggerDistance;

    public static float arrowReleaseTime = 3.05f;

    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        archerGameObject = this.gameObject;
        animator = GetComponent<Animator>();

        ChangeState(patrolState);
    }

    public void ChangeState(ArcherBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void Update()
    {
        if (target)
        {
            // Face the target
            Vector3 direction = target.transform.position - archerGameObject.transform.position;
            // Need to modify the angle by 90 degrees otherwise the bow will not face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 90f, 0f);
            archerGameObject.transform.rotation = Quaternion.Slerp(archerGameObject.transform.rotation, targetRotation, 4f * Time.deltaTime);
        }
    }

    // Accessed by animation events
    public void ShootArrow()
    {
        Quaternion arrowRotation = arrowRenderer.gameObject.transform.rotation;
        GameObject arrow = Instantiate(ArrowPrefab, arrowRenderer.gameObject.transform.position, arrowRotation);
        arrow.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * arrowSpeed, ForceMode.Impulse);
        Destroy(arrow, 2.5f);
    }

    public void ToggleArrow()
    {
        arrowRenderer.enabled = !arrowRenderer.enabled;
    }
}
