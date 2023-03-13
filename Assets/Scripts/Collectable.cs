using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectable : MonoBehaviour
{

    public Stockpile goldStockpile;
    public Stockpile stoneStockpile;
    
    enum CollectableType {
        Stone,
        Gold
    }

    [SerializeField] private CollectableType collectableType;

    [SerializeField] private AudioSource PickSound;

    public void Interact() {
        StartCoroutine(CollectablePickUp(.2f));
    }

    IEnumerator CollectablePickUp(float _delay) {
        PickSound.enabled = true;
        if (collectableType == CollectableType.Stone) {
            int randomStone = Random.Range(1, 3);
            Player.Instance.Stone = Player.Instance.Stone + randomStone;

            for (int i = 0; i < randomStone; i++) {
                stoneStockpile.SpawnCollectedStone();
            }
        } else if (collectableType == CollectableType.Gold) {
            int randomGold = Random.Range(1, 3);
            Player.Instance.Gold = Player.Instance.Gold + randomGold;
            
            for (int i = 0; i < randomGold; i++) {
                goldStockpile.SpawnCollectedGold();
            }
        }
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
