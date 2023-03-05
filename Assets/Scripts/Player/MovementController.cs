using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public Rigidbody2D rigidBody;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    public AudioSource soundRun;
    [SerializeField]
    public JumpController jumpController;

    public float horizontal;
    public bool isFaceRight = true;
    public bool isRuning;

    [SerializeField]
    public PlayerController playerController;
    private JoystickController joystickController;

    void Start()
    {
        joystickController = GetComponent<JoystickController>();
        if (playerController == null)
        {
            playerController = gameObject.GetComponent<PlayerController>();
        }
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        horizontal = joystickController.GetHorizontalValue();

        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
        if (horizontal != 0)
        {
            anim.SetBool("isRunning", true);
            isRuning = true;
            if (!soundRun.isPlaying && isRuning && jumpController.isGrounded)
            {
                soundRun.pitch = 1.5f;
                soundRun.Play();
                Debug.Log("Running sound play");
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            isRuning = false;
            soundRun.Stop();
            Debug.Log("Running sound stop");
        }

        Flip();
    }

    public void Flip()
    {
        if (isFaceRight && horizontal < 0f  || !isFaceRight && horizontal> 0f)
        {
            isFaceRight = !isFaceRight;
            if (horizontal < 0)
            {
                isFaceRight = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (horizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
