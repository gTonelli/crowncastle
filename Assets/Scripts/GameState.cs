using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject gameOverCanvas;

    [SerializeField]
    private int playerHealth;
    private float timeOfLastHit;
    private float minTimeBetweenPlayerHit;

    //public delegate void ChangeToNightTime();
    //public static event ChangeToNightTime OnChangeToNightTime;

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

            if (playerHealth <= 0)
            {
                Time.timeScale = 0.1f;
                gameOverCanvas.SetActive(true);
            }
        }
    }

    void Update()
    {
    }

    public void RestartGame()
    {
        print("Yo");
    }

    public void QuitGame()
    {
        print("Yo");
    }

}