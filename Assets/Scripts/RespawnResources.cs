using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnResources : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject playerPrefab;

    TimeController timeController = new TimeController();

    private bool IsMined;

    public DateTime cT;

    public void Mined()
    {
        cT = timeController.currentTime;
        IsMined = true;
    }

    public void Respawn()
    {

        if (cT.AddHours(8) <= timeController.currentTime && IsMined)
        {
            IsMined = false;

            cT = timeController.currentTime;

        }

    }
    private void OnEnable()
    {
        StonePile.Mined += Respawn;
    }

    private void OnDisabled()
    {
        StonePile.Mined -= Respawn;
    }
}
