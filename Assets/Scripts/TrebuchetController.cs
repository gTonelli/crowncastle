using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetController : MonoBehaviour
{

    public Rigidbody weight;
    public GameObject pumpkin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            //release the weight
            weight.isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //launch pumpkin

            HingeJoint hingeToDestroy;
            hingeToDestroy = pumpkin.GetComponent<HingeJoint>();

            Destroy(hingeToDestroy);
        }
    }
}
