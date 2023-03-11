using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StonePile : MonoBehaviour
{
    [SerializeField]
    public GameObject playerPrefab;

    GameObject playerInstance;

    public bool miningCommand = false;

    private int miningProgress = 1;

    private int miningLimit = 8;

    [SerializeField]
    private float amount = 10;

    public event EventHandler<OnProgressChangedEventArgs> OnStonePileChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalised;
    }

    public delegate void MiningDone();
    public static event MiningDone Mined;

    private void Start()
    {
        DestroySelf();
        Mined?.Invoke();
    }
    public void Interact()
    {

        miningCommand = true;
        Debug.Log("Interacting with stone pile");

        OnStonePileChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalised = (float)miningProgress / miningLimit });

        miningProgress++;
        amount++;
        Debug.Log(miningProgress);
        if (miningProgress == miningLimit)
        {
            //mined
            DestroySelf();
            Mined?.Invoke();
        }


    }


    public float GetAmount()
    {
        return amount;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public bool commandGiven()
    {
        return miningCommand;
    }

    public void Respawn()
    {
       /* if (playerInstance == null)
            playerInstance = Instantiate(playerPrefab);
        Instantiate(gameObject);*/
    }

    private void OnEnable()
    {
        /*TimeController.Resources += Respawn;*/
    }

    private void OnDisabled()
    {
/*        TimeController.Resources -= Respawn;*/
    }


}