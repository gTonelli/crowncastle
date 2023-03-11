using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultOperator : MonoBehaviour
{

    public Rigidbody weight;
    public GameObject cannonball;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("GroundShattering").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //release the weight
            weight.isKinematic = false;

            //launch cannonball
            FixedJoint fixedToDestroy;
            fixedToDestroy = cannonball.GetComponent<FixedJoint>();

            Destroy(fixedToDestroy);
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //play explosion
            ps.Play();
            Destroy(cannonball.gameObject);
        }

    }
}
