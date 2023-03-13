using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultGuy : MonoBehaviour
{

    [SerializeField] CatapultOperator catapult;
    [SerializeField] TrebuchetOperator trebuchet;
    [SerializeField] MangonelOperator mangonel;
    [SerializeField] private AudioSource NotEnoughSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        Debug.Log("Interacted with");
        if (Player.Instance.Gold >= 5) {
            Player.Instance.Gold = Player.Instance.Gold - 5;
            if (catapult != null) {
                catapult.activate = true;
            }
            if (trebuchet != null) {
                trebuchet.activate = true;
            }
            if (mangonel != null) {
                mangonel.activate = true;
            }

        } else if (Player.Instance.Gold < 5) {
            NotEnoughSound.enabled = true;
            NotEnoughSound.Play();
        }
        
    }
}
