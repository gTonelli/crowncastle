using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemySpawner : MonoBehaviour
{
    public int numEnemiesToSpawn;
    public GameObject EnemyPrefab;
    private bool enemiesAreSpawning;

    private void OnEnable()
    {
        EventManager.OnChangeToNightTime += SpawnSkeletons;
    }

    private void OnDisable()
    {
        EventManager.OnChangeToNightTime -= SpawnSkeletons;
    }

    // Start is called before the first frame update
    void Start()
    {
        numEnemiesToSpawn = 2;
        enemiesAreSpawning = false;
    }

    private async void SpawnSkeletons()
    {
        if (!enemiesAreSpawning)
        {
            enemiesAreSpawning = true;
            for (int _ = 0; _ < numEnemiesToSpawn; ++_)
            {
                await Task.Delay(2500 + Random.Range(0, 1250));
                Instantiate(EnemyPrefab, transform.position, new Quaternion(0f, -1f, 0f, 1f));
            }
            enemiesAreSpawning = false;
            Debug.Log("\\n\nEnemies Done spawning\n\\n");
        }

    }
}
