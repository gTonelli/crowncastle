using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Threading.Tasks;
using Vector3 = UnityEngine.Vector3;

public class StonePile : MonoBehaviour {

    public bool miningCommand = false;

    public int miningProgress = 0;

    public int miningLimit = 5;

    public bool destroyThis = false;

    public bool interacted = false;

    [SerializeField] GameObject DestroyVFX;
    [SerializeField] GameObject DropCollectable;

    public event EventHandler<OnProgressChangedEventArgs> OnStonePileChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalised;
    }

    public void Interact() {
        if(!interacted) {
            interacted = true;
            miningCommand = true;
            StartCoroutine(DelayMining(1f));
        }
    }

    IEnumerator DelayMining(float _delay) {
        for (int i = 0; i < miningLimit; ++i) {
            yield return new WaitForSeconds(_delay);
            miningProgress++;
            //Debug.Log("miningProgress: " + miningProgress);
            OnStonePileChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalised = (float)miningProgress / miningLimit });
        }

        Vector3 dropPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);

        if (miningProgress > miningLimit || miningProgress == miningLimit) {
            destroyThis = true;
            GameObject explosion = Instantiate(DestroyVFX, transform.position, transform.rotation);
            GameObject dropCollectable = Instantiate(DropCollectable, dropPos, transform.rotation);
            yield return new WaitForSeconds(0.01f);
            //mined
            DestroySelf();
        }
        
        //Debug.Log("Now delay one is finish");
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public bool commandGiven() {
        return miningCommand;
    }

    public void Update() {
        
    }

}
