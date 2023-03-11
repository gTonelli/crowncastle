using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangonelOperator : MonoBehaviour
{

    public Rigidbody weight;
    public GameObject cannonball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            //release the weight
            weight.isKinematic = false;

            //launch cannonball
            /*FixedJoint fixedToDestroy;
            fixedToDestroy = cannonball.GetComponent<FixedJoint>();

            Destroy(fixedToDestroy);*/
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //destroy projectiles
            //Destroy(cannonball.gameObject);
        }
    }
}
