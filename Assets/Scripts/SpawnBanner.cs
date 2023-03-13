using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnBanner : MonoBehaviour
{
    [SerializeField] private AudioSource NotEnoughSound;
    public Stockpile goldStockpile;

    [SerializeField] private GameObject Archer;
    private GameObject newArcher = null;

    public void Interact() {
        if (Player.Instance.Gold >= 2) {
            Player.Instance.Gold = Player.Instance.Gold - 2;
            StartCoroutine(SpawnArchers(.2f));
            
            foreach (GameObject deleteGold in Stockpile.goldStock.stockpileGoldPieces) {
                Destroy(deleteGold);
            }

        } else if (Player.Instance.Gold < 2) {
            NotEnoughSound.enabled = true;
            NotEnoughSound.Play();
        }
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
