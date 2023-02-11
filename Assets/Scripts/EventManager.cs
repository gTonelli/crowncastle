using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void ChangeToNightTime();
    public static event ChangeToNightTime OnChangeToNightTime;

    void Update()
    {
        if (Input.GetButtonUp("Fire1")) // #TODO Replace 
        {
            OnChangeToNightTime?.Invoke(); // #TODO 
        }
    }
}