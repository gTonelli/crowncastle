using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class ArcherFSM : MonoBehaviour
{
    private ArcherBaseState currentState;
    public ArcherBaseState CurrentState
    {
        get { return currentState; }
    }

    public ArcherPatrolState patrolState = new ArcherPatrolState();
    public ArcherAttackState attackState = new ArcherAttackState();

    [Header("Assigned At Run-time")]
    public GameObject archerGameObject;
    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Assigned In Editor")]
    public GameObject ArrowPrefab;
    public float moveSpeed;
    public float waitTime;
    public float attackTriggerDistance;
    public float arrowSpeed;

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
                animationClip = clip;
                animationClip.AddEvent(shootEvent);
                break;
            }
        }

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
        if (target)
        {
            Quaternion arrowRotation = arrowRenderer.gameObject.transform.rotation;
            GameObject arrow = Instantiate(ArrowPrefab, arrowRenderer.gameObject.transform.position, arrowRotation);
            arrow.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * arrowSpeed, ForceMode.Impulse);
            Destroy(arrow, 2.5f);
        }
    }

    public void ToggleArrow()
    {
        arrowRenderer.enabled = !arrowRenderer.enabled;
    }
}
