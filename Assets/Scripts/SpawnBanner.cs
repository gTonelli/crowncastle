using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBanner : MonoBehaviour
{
    [SerializeField] private GameObject Archer;
    private GameObject newArcher = null;

    public void Interact() {
        StartCoroutine(SpawnArchers(.2f));
    }

    IEnumerator SpawnArchers(float _delay) {
        
        yield return new WaitForSeconds(_delay);
        Vector3 dropPos = new Vector3(transform.position.x + Random.Range(1.5f, 3.5f), transform.position.y, transform.position.z - Random.Range(1.5f, 3.5f));
        newArcher = Instantiate(Archer, dropPos, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
