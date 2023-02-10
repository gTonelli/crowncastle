using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        }
        catch (System.Exception)
        {
            Debug.LogError("ERROR: Player not in Game Scene or Transform not present");
            throw;
        }

        try
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
        catch (System.Exception)
        {
            Debug.LogError("SKELETON: Nav Mesh Agent or Animator not present");
            throw;
        }
    }

    void Update()
    {
        if (Time.frameCount % 240 == 40) WalkToPlayer();
    }

    private void WalkToPlayer()
    {
        try
        {
            animator.SetBool("isWalking", true);
            navMeshAgent.SetDestination(playerTransform.position);
        }
        catch (System.Exception)
        {
            Debug.LogError("SKELETON: Pathfinding or movement error.");
            throw;
        }
    }
}
