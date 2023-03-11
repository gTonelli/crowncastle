using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemySpawner : MonoBehaviour
{
    public int numEnemiesToSpawn = 10;
    public GameObject EnemyPrefab;
    private bool enemiesAreSpawning;
    private Vector3 lastEnemyPos; // #TODO prevent enemies from spawning on each other.

    private void OnEnable()
    {
        TimeController.OnChangeToNightTime += SpawnSkeletons;
    }

    private void OnDisable()
    {
        TimeController.OnChangeToNightTime -= SpawnSkeletons;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemiesAreSpawning = false;
    }

    private async void SpawnSkeletons()
    {
        //Instantiate(EnemyPrefab, transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1.5f, 1.5f)), new Quaternion(0f, -1f, 0f, 1f));

        if (!enemiesAreSpawning)
        {
            enemiesAreSpawning = true;
            for (int _ = 0; _ < numEnemiesToSpawn; ++_)
            {
                await Task.Delay(2500 + Random.Range(0, 1250));
                // #TODO
                Instantiate(EnemyPrefab, transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-1.5f, 1.5f)), new Quaternion(0f, -1f, 0f, 1f));
            }
        }

    }
}
