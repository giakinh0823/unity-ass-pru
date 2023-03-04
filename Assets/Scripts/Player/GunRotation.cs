using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public float angle;
    public float rotationSpeed = 50f;
    [SerializeField]
    public PlayerController playerController;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public GameObject gun;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Joystick joystick = playerController.joystick;
        animator.SetFloat("JoystickVertical", joystick.Vertical);
        Debug.Log(joystick.Vertical);
    }
}
