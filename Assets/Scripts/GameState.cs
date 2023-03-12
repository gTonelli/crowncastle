﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject resourceCanvas;

    [SerializeField]
    private int playerHealth;
    private float timeOfLastHit;
    private float minTimeBetweenPlayerHit;
    private int gold;

    public delegate void PurchaseArcher();
    public static event PurchaseArcher OnPurchaseArcher;

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
        gold = 0;
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

    public void RestartGame()
    {
        // TODO
        // ...
    }

    public void QuitGame()
    {
        // TODO
        // ...
    }



}