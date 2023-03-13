using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetOperator : MonoBehaviour
{
    public Rigidbody weight;
    public GameObject projectile;
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

        if (activate) {
            kinematicstart++;
            //release the weight
            if (kinematicstart == 1) {
                weight.isKinematic = false;
            }
            StartCoroutine(LaunchTrebuchet(0.7f));
        }

        IEnumerator LaunchTrebuchet(float _delay) {

            yield return new WaitForSeconds(_delay);
            HingeJoint hingeToDestroy;
            if (projectile != null) {
                hingeToDestroy = projectile.GetComponent<HingeJoint>();

                Destroy(hingeToDestroy);
                StartCoroutine(detach(0.5f));
            }
        }
        
        IEnumerator detach(float _delay) {

            yield return new WaitForSeconds(_delay);
            weight.isKinematic = true;
            if (projectile != null) {
                if (projectile.transform.position.y < 0.25) {
                    Vector3 dropPos = new Vector3(projectile.transform.position.x, projectile.transform.position.y, projectile.transform.position.z);
                    ParticleSystem dropParticle = Instantiate(ps, dropPos, transform.rotation);
                    dropParticle.Play();
                    Destroy(projectile.gameObject);
                }
            }
        }

        
    }
}
