using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangonelOperator : MonoBehaviour
{

    public Rigidbody weight;
    public GameObject cannonball, cannonball1, cannonball2, cannonball3, cannonball4, cannonball5;
    public bool activate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate) {
            
            //release the weight
            weight.isKinematic = false;

            //launch cannonballs
            FixedJoint fixedToDestroy, fixedToDestroy1, fixedToDestroy2, fixedToDestroy3, fixedToDestroy4, fixedToDestroy5;
            fixedToDestroy = cannonball.GetComponent<FixedJoint>();
            fixedToDestroy1 = cannonball1.GetComponent<FixedJoint>();
            fixedToDestroy2 = cannonball2.GetComponent<FixedJoint>();
            fixedToDestroy3 = cannonball3.GetComponent<FixedJoint>();
            fixedToDestroy4 = cannonball4.GetComponent<FixedJoint>();
            fixedToDestroy5 = cannonball5.GetComponent<FixedJoint>();

            Destroy(fixedToDestroy);
            Destroy(fixedToDestroy1);
            Destroy(fixedToDestroy2);
            Destroy(fixedToDestroy3);
            Destroy(fixedToDestroy4);
            Destroy(fixedToDestroy5);
        }
    }
}
