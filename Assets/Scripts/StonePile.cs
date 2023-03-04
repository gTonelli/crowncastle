using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePile : MonoBehaviour
{

    public bool miningCommand = false;

    private int miningProgress = 1;

    private int miningLimit = 8;

    public event EventHandler<OnProgressChangedEventArgs> OnStonePileChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalised;
    }

    public  void Interact() {

        miningCommand = true;
        Debug.Log("Interacting with stone pile");
        
        OnStonePileChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalised = (float)miningProgress / miningLimit });
        
        miningProgress++;
        Debug.Log(miningProgress);
        if (miningProgress == miningLimit) {
            //mined
            DestroySelf();
        }
        
    }

    


    public void DestroySelf() {
        Destroy(gameObject);
    }

    public bool commandGiven() {
        return miningCommand;
    }

}
