using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using Task = System.Threading.Tasks.Task;

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

    private async void Start()
    {
        anim      = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody2D>();

        while (InputManager.Instance == null)
        {
            await Task.Delay(100);
        }
        InputManager.Instance.jump += this.OnJump;
    }

    private void Update()
    {
        isGrounded = IsGrounded();

        if (this.isGrounded)
        {
            doubleJump = false;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
            soundRun.Stop();
        }

        if (this.isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity =  Vector2.up * jumpForce;
                jumpTimeCounter    -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (isGrounded == false && doubleJump == false)
        {
            isJumping          = true;
            doubleJump         = true;
            isJumping          = true;
            jumpTimeCounter    = jumpTime;
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

    private void OnJump()
    {
        if (isGrounded)
        {
            anim.SetTrigger("takeOf");
            isJumping          = true;
            jumpTimeCounter    = jumpTime;
            rigidBody.velocity = Vector2.up * jumpForce;
        }
    }
}