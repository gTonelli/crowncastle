using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{


    [Header("Spawn settings")]
    public GameObject resourcePrefab;
    public float spawnChance;

    [Header("Raycast Settings")]
    public float distanceBetweenChecks;
    public float heightOfCheck = 10f, rangeOfCheck;
    public LayerMask layerMask;
    public Vector2 positivePosition, negativePosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnResources();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnResources()
    {
        for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks)
        {
            for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(x, heightOfCheck, z),Vector3.down, out hit, rangeOfCheck, layerMask))
                {
                    if (spawnChance > Random.Range(0, 101))
                    {
                        Instantiate(resourcePrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                    }
                }
            }
        }
    }

    void Respawn()
    {

    }
}

