using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class ArcherFSM : MonoBehaviour
{
    private int health;
    private ArcherBaseState currentState;
    public ArcherBaseState CurrentState
    {
        get { return currentState; }
    }

    public ArcherPatrolState patrolState = new ArcherPatrolState();
    public ArcherAttackState attackState = new ArcherAttackState();
    public ArcherIdleState idleState = new ArcherIdleState();

    [Header("Assigned At Run-time")]
    public GameObject archerGameObject;
    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;
    private float timeSinceLastHit;
    private float timeBetweenHits;

    [Header("Assigned In Editor")]
    public GameObject ArrowPrefab;
    public float moveSpeed;
    public float waitTime;
    public float attackTriggerDistance;
    public float arrowSpeed;
    public bool canPatrol = true;

    // This is the time in the animation when the arrow should be released. Also accessed by ToggleArrowVisibility.cs
    [HideInInspector]
    public static float arrowReleaseTime = 3.05f;

    [SerializeField]
    private Renderer arrowRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // init
        agent = GetComponent<NavMeshAgent>();
        archerGameObject = this.gameObject;
        animator = GetComponent<Animator>();
        health = 2;
        timeSinceLastHit = Time.time;
        timeBetweenHits = 1.33f;

        foreach (SkinnedMeshRenderer _ in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (_.gameObject.CompareTag("Arrow"))
            {
                arrowRenderer = _.gameObject.GetComponent<Renderer>();
                break;
            }
        }

        // Add shoot arrow event to attack animation
        AnimationClip animationClip;
        AnimationEvent shootEvent = new AnimationEvent();
        shootEvent.time = arrowReleaseTime;
        shootEvent.functionName = "ShootArrow";

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "ShootingArrow")
            {
                if (clip.events.Length < 3)
                {
                    animationClip = clip;
                    animationClip.AddEvent(shootEvent);
                    break;
                }
            }
        }

        ChangeState(idleState);
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
        else if (currentState == attackState) ChangeState(idleState);
    }

    // Accessed by animation events
    public void ShootArrow()
    {
        if (target)
        {
            Quaternion arrowRotation = arrowRenderer.gameObject.transform.rotation;
            GameObject arrow = Instantiate(ArrowPrefab, arrowRenderer.gameObject.transform.position, arrowRotation);
            arrow.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * arrowSpeed, ForceMode.Impulse);
            Destroy(arrow, 2.5f);
        }
    }

    public void ToggleArrowOff()
    {
        arrowRenderer.enabled = false;
    }

    public void ToggleArrowOn()
    {
        arrowRenderer.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision archer:" + other.tag);
        if (other.CompareTag("Skeleton") && Time.time - timeBetweenHits > timeSinceLastHit)
        {
            health -= 1;
            timeSinceLastHit = Time.time;
            if (health <= 0)
            {
                // Disable animator and AI Agent to ragdoll the skeleton.
                animator.enabled = false;
                agent.enabled = false;
                Destroy(gameObject, 1.5f);
            }
        }
    }
}
