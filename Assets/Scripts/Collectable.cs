using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectable : MonoBehaviour
{
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
            Player.Instance.Stone = Player.Instance.Stone + Random.Range(1, 3);
        } else if (collectableType == CollectableType.Gold) {
            Player.Instance.Gold = Player.Instance.Gold + Random.Range(1, 3);
        }
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
