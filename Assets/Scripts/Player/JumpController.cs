using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    [SerializeField]
    public float jumpForce;
    [SerializeField]
    public Rigidbody2D rigidBody;
    private Animator anim;

    [SerializeField]
    public Transform groundPos;
    public bool isGrounded;
    [SerializeField]
    public float checkRadius;
    [SerializeField]
    public LayerMask whatIsGround;
    [SerializeField]
    public AudioSource soundRun;

    [SerializeField]
    public AudioSource soundJumpDown;

    private float jumpTimeCounter;
    [SerializeField]
    public float jumpTime;
    private bool isJumping;
    private bool doubleJump;

    private MyPlayerActions playerInput;
    private InputAction jumpInput;

    private void Awake()
    {
        playerInput = new MyPlayerActions();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        isGrounded = IsGrounded();

        if (isGrounded == true && jumpInput.triggered)
        {
            anim.SetTrigger("takeOf");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded == true)
        {
            doubleJump = false;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
            soundRun.Stop();
        }


        if (jumpInput.triggered && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (jumpInput.triggered)
        {
            isJumping = false;

        }

        if (isGrounded == false && doubleJump == false && jumpInput.triggered)
        {
            isJumping = true;
            doubleJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = Vector2.up * jumpForce;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);
    }

    public void playSoundJumpDown()
    {
        soundJumpDown.Play();
    }

    private void OnEnable()
    {
        jumpInput = playerInput.Player.Jump;
        jumpInput.Enable();
    }

    private void OnDisable()
    {
        jumpInput = playerInput.Player.Jump;
        jumpInput.Disable();
    }

}