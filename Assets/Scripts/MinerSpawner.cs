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
    public GameObject miner;
    //private Vector3 lastMinerPos; // #TODO prevent miners from spawning on each other.
    public static List<GameObject> miners = new List<GameObject>();

    private void Update() {
        if (stonePile.commandGiven()) {
            SpawnMiners();
        };

        if (stonePile.destroyThis) {
            DestroyMiners();
        }
            

        /*if (stonePile.miningProgress > stonePile.miningLimit - 1 || stonePile.miningProgress == stonePile.miningLimit - 1) {
            DestroyMiners();
        }*/
    }

    // Start is called before the first frame update
    void Start() {
        minersAreSpawning = false;
    }

    private async void SpawnMiners() {
        Vector3 minerPos = new Vector3(Random.Range(1f, 2.4f), 0f, Random.Range(1f, 1.5f));
        Quaternion minerRot = new Quaternion(0f, 0f, 0f, 0f);
        miner = Instantiate(MinerPrefab, transform.position + minerPos, minerRot);

        Vector3 dirFromStonePile = miner.transform.position - transform.position;
        miner.transform.LookAt(miner.transform.position - dirFromStonePile);

        StartCoroutine(DelaySpawning(.75f));
        miners.Add(miner);

        stonePile.miningCommand = false;
        
    }

    IEnumerator DelaySpawning(float _delay) {
        
        for (int i = 0; i < 3; ++i) {
            yield return new WaitForSeconds(_delay);
            Vector3 minerPos = new Vector3(Random.Range(-2.4f, 2.4f), 0f, Random.Range(-1.5f, 1.5f));
            Quaternion minerRot = new Quaternion(0f, 0f, 0f, 0f);
            miner = Instantiate(MinerPrefab, transform.position + minerPos, minerRot);
            miners.Add(miner);

            Vector3 dirFromStonePile = miner.transform.position - transform.position;
            miner.transform.LookAt(miner.transform.position - dirFromStonePile);
        } 
    }

    public void DestroyMiners() {
        foreach (GameObject miner in miners) {
            Destroy(miner);
        }
    }
}
