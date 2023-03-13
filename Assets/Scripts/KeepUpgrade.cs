using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        if (Player.Instance.Gold >= 10) {
            Player.Instance.Gold = Player.Instance.Gold - 10;
        }
    }

}
