using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour
{
    public GameObject Gold;
    
    public static Stockpile goldStock;  // <-- creates a static variable
    
    public List<GameObject> stockpileGoldPieces = new List<GameObject>();

    public GameObject Stone;

    public static Stockpile stoneStock;  // <-- creates a static variable

    public List<GameObject> stockpileStonePieces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Stockpile.goldStock = (Stockpile)this;
        Stockpile.stoneStock = (Stockpile)this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCollectedGold() {
        // spawn gold
        Vector3 dropPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y, transform.position.z - Random.Range(-1.5f, 1.5f));
        GameObject stockpileGold = Instantiate(Gold, dropPos, transform.rotation);
        stockpileGoldPieces.Add(stockpileGold);
    }

    public void SpawnCollectedStone() {
        // spawn Stone
        Vector3 dropPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y, transform.position.z - Random.Range(-1.5f, 1.5f));
        GameObject stockpileStone = Instantiate(Stone, dropPos, transform.rotation);
        stockpileStonePieces.Add(stockpileStone);
    }


}
