using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{



    public GameObject resourcePrefab;


    



  

    public void Respawn()
    {
        Instantiate(resourcePrefab);
    }
}

