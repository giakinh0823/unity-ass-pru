using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float horizontal;
    public bool isFaceRight = true;
    public bool isRuning;

    [SerializeField]
    public PlayerController playerController;

    private Joystick joystick;

    private void Start()
    {
        if(playerController == null)
        {
            playerController = gameObject.GetComponent<PlayerController>();
        }
        joystick = playerController.joystick;
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal == 0)
        {
            horizontal = Mathf.Round(joystick.Horizontal);
        }

        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
        if (horizontal != 0)
        {
            anim.SetBool("isRunning", true);
            isRuning = true;
            if (!soundRun.isPlaying)
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
