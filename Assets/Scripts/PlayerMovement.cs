using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 1f;
    public float turnSmoothSpeed = 0.1f;
    float turnSmoothSpeedOut;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothSpeedOut, turnSmoothSpeed);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
