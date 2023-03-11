using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetOperator : MonoBehaviour
{
    public Rigidbody weight;
    public GameObject projectile;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("GroundExplosion").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //release the weight
            weight.isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //launch projectile

            HingeJoint hingeToDestroy;
            hingeToDestroy = projectile.GetComponent<HingeJoint>();

            Destroy(hingeToDestroy);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //play explosion
            ps.Play();
            Destroy(projectile.gameObject);
        }
    }
}
