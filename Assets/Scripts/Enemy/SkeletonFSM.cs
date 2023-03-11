using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SkeletonFSM : MonoBehaviour
{

    private SkeletonBaseState currentState;
    public SkeletonBaseState CurrentState
    {
        get { return currentState; }
    }

    public SkeletonMoveState moveState = new SkeletonMoveState();
    public SkeletonAttackState attackState = new SkeletonAttackState();

    [Header("Assigned at Run-time")]
    public GameObject skeletonGameObject;
    public GameObject target;
    public Animator animator;

    [Header("Assigned in Editor")]
    public ParticleSystem arrowHitEffect;
    public float moveSpeed;
    public float waitTime;
    public float attackTriggerDistance;
    public float chaseTriggerDistance;

    public NavMeshAgent agent;

    private int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        skeletonGameObject = this.gameObject;
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");

        ChangeState(moveState);
    }

    private void DamageSkeleton(int damage)
    {
        health -= damage;
        print("Skeleton has" + health);
        if (health <= 0)
        {
            // Disable animator and AI Agent to ragdoll the skeleton.
            animator.enabled = false;
            agent.enabled = false;
            Destroy(this.gameObject, 1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false; // Remove the collider so it doesn't strike more than 1 skeleton.
            DamageSkeleton(1);
            ParticleSystem particleEffect = Instantiate(arrowHitEffect, collision.GetContact(0).point, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject, 0.05f);
            Destroy(particleEffect, 0.1f);
        }

    }

    public void ChangeState(SkeletonBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
