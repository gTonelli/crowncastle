using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITurnCamera : MonoBehaviour
{
    // Update is called once per frame
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
