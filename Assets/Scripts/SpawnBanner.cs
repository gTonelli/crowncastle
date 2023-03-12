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
        newArcher = Instantiate(Archer, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
