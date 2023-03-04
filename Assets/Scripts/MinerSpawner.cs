using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MinerSpawner : MonoBehaviour
{
    //public int numMinersToSpawn;
    public GameObject MinerPrefab;
    [SerializeField] private Player player;
    private bool minersAreSpawning;
    [SerializeField] private StonePile stonePile;
    //private Vector3 lastMinerPos; // #TODO prevent miners from spawning on each other.

    private void Update() {
        if (stonePile.commandGiven()) {
            Debug.Log("HUAAA REEE");
            SpawnMiners();
        };
    }

    // Start is called before the first frame update
    void Start() {
        minersAreSpawning = false;
    }

    private async void SpawnMiners() {
        Instantiate(MinerPrefab, transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1.5f, 1.5f)), new Quaternion(0f, -1f, 0f, 1f));
        stonePile.miningCommand = false;

        // if (!minersAreSpawning)
        // {
        //     minersAreSpawning = true;
        //     for (int _ = 0; _ < numMinersToSpawn; ++_)
        //     {
        //         await Task.Delay(2500 + Random.Range(0, 1250));
        //         // #TODO
        //         Instantiate(MinerPrefab, transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1.5f, 1.5f)), new Quaternion(0f, -1f, 0f, 1f));
        //     }
        //     minersAreSpawning = false;
        //     Debug.Log("Miners Done spawning");
        // }

    }
}
