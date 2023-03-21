using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlidingController : MonoBehaviour
{
    public bool isWallSliding;
    private float wallSlidingSpeed;

    private bool isWallJumping;
    private float isWallJumpingSpeed;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration =0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private JumpController jump;
    [SerializeField]
    private MovementController movement;
    [SerializeField]
    private AudioSource wallSlidingSound;

    private void Start()
    {
        anim                                  =  GetComponent<Animator>();
        FindObjectOfType<InputManager>().jump += this.OnJump;
    }

    private void OnJump()
    {
        if(wallJumpingCounter > 0f) {
            isWallJumping = true;
            anim.SetBool("isWallJumping", true);
            anim.SetBool("isWallSliding", false);
            rigidBody.velocity = new Vector2(wallJumpingDuration * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                movement.isFaceRight = !movement.isFaceRight;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        } 
        else
        {
            anim.SetBool("isWallJumping", false);
        }
    }

    private void Update()
    {
        WallSlide();
        WallJump();

        if(!isWallJumping)
        {
            movement.Flip();
        } 

    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rigidBody.velocity = new Vector2(movement.horizontal * movement.speed, rigidBody.velocity.y);
        } 
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            anim.SetBool("isWallJumping", false);
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        } else
        {
            wallJumpingCounter -= Time.deltaTime;
            anim.SetBool("isWallJumping", true);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
        anim.SetBool("isWallJumping", false);
    }

    private void WallSlide()
    {
        if(IsWalled() && !jump.IsGrounded() && movement.horizontal != 0f)
        {
            isWallSliding= true;
            anim.SetBool("isWallSliding", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        } else
        {
            isWallSliding= false;
            anim.SetBool("isWallSliding", false);
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    public void playSoundWallSliding()
    {
        wallSlidingSound.Play();
    }
}
