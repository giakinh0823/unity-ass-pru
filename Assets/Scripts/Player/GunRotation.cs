using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public float angle;
    public float rotationSpeed = 50f;
    [SerializeField]
    public PlayerController playerController;


    void Update()
    {
        Joystick joystick = playerController.joystick;
        angle = Mathf.Atan2(joystick.Direction.y, joystick.Direction.x) * Mathf.Rad2Deg;
        var lookRotation = Quaternion.Euler(angle * Vector3.forward);
        transform.rotation = lookRotation;
    }
}
