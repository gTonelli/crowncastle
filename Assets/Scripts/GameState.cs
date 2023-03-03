using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private int playerHealth;
    private float timeOfLastHit;
    private float minTimeBetweenPlayerHit;

    public delegate void ChangeToNightTime();
    public static event ChangeToNightTime OnChangeToNightTime;

    private void OnEnable()
    {
        SkeletonSword.OnPlayerHit += DamagePlayer;
    }

    private void OnDisable()
    {
        SkeletonSword.OnPlayerHit -= DamagePlayer;
    }

    void Start()
    {
        playerHealth = 5;
        timeOfLastHit = Time.time;
        minTimeBetweenPlayerHit = 2f;
    }

    void DamagePlayer()
    {
        if (Time.time - minTimeBetweenPlayerHit > timeOfLastHit)
        {
            playerHealth -= 1;
            timeOfLastHit = Time.time;
            Debug.Log("Player was hit and has " + playerHealth + " HP");

            if (playerHealth <= 0)
            {
                // TODO
                Debug.Log("Game Over");
                Time.timeScale = 0.1f;
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1")) // #TODO Replace 
        {
            OnChangeToNightTime?.Invoke(); // #TODO 
        }
    }

}
