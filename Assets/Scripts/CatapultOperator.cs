using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class CatapultOperator : MonoBehaviour
{

    public Rigidbody weight;
    public GameObject cannonball;
    public ParticleSystem ps;
    public bool activate = false;
    private int kinematicstart = 0;
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("weight.isKinematic: " + weight.isKinematic);

        if (activate)
        {
            kinematicstart++;
            //release the weight
            if (kinematicstart == 1) {
                weight.isKinematic = false;
            }

            //launch cannonball
            FixedJoint fixedToDestroy;
            if (cannonball != null) {
                fixedToDestroy = cannonball.GetComponent<FixedJoint>();
                Destroy(fixedToDestroy);
                StartCoroutine(AfterLaunch(0.4f));
            }
            
        }

        IEnumerator AfterLaunch(float _delay) {

            yield return new WaitForSeconds(_delay);
            weight.isKinematic = true;
        }
        
        if (cannonball != null) {
            if (cannonball.transform.position.y < 1) {
                Vector3 dropPos = new Vector3(cannonball.transform.position.x, cannonball.transform.position.y, cannonball.transform.position.z);
                ParticleSystem dropParticle = Instantiate(ps, dropPos, transform.rotation);
                dropParticle.Play();
                Destroy(cannonball.gameObject);
            }
        }
    }
}
