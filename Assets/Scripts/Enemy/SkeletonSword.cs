using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    public delegate void PlayerHit();
    public static event PlayerHit OnPlayerHit;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerHit?.Invoke();
        }
    }
}
