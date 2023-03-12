using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileSpawner : MonoBehaviour
{
    [SerializeField] private StonePile stonePile;
    private StonePile newPile = null;
    public bool pileSpawned;
    public float SpawnDelay;

    void Start()
    {
        pileSpawned = false;
        SpawnDelay = Random.Range(10f, 20f);
        StartCoroutine(SpawnPile(SpawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SpawnDelay);
        if (pileSpawned) {
            checkIfPileExists();
        }
    }

    IEnumerator SpawnPile(float _delay) {
        yield return new WaitForSeconds(_delay);
        if (!pileSpawned) {
            pileSpawned = true;
            newPile = Instantiate(stonePile, transform.position, transform.rotation);
        }
    }

    void checkIfPileExists() {
        if (newPile == null) {
            pileSpawned = false;
            SetNewSpawnDelay();
            StartCoroutine(SpawnPile(SpawnDelay));
        }
    }

    void SetNewSpawnDelay() {
        SpawnDelay = Random.Range(6f, 50f);
    }
}
