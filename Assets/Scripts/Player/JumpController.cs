using Common;
using UnityEngine;

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

    private void Start()
    {
        anim      = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody2D>();

        FindObjectOfType<InputManager>().jump += this.OnJump;
    }

    private void Update()
    {
        isGrounded = IsGrounded();

        if (this.isGrounded)
        {
            doubleJump      = false;
            jumpTimeCounter = jumpTime;
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
                jumpTimeCounter    -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (!isGrounded && doubleJump == false)
        {
            isJumping          = true;
            doubleJump         = true;
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
        if (isGrounded || (this.isJumping && this.jumpTimeCounter > 0))
        {
            anim.SetTrigger("takeOf");
            isJumping          = true;
            rigidBody.velocity = Vector2.up * jumpForce;
        }
    }
}