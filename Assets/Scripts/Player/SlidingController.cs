using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class SlidingController : MonoBehaviour
{
    private bool isWallSliding;
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

    private void Start()
    {
        anim = GetComponent<Animator>();
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
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        } else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Z) && wallJumpingCounter > 0f ) {
            isWallJumping = true;
            anim.SetTrigger("takeOf");
            anim.SetBool("IsWallSliding", false);
            rigidBody.velocity = new Vector2(wallJumpingDuration * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                movement.isFaceRight = !movement.isFaceRight;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void WallSlide()
    {
        if(IsWalled() && !jump.IsGrounded() && movement.horizontal != 0f)
        {
            isWallSliding= true;
            anim.SetBool("IsWallSliding", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        } else
        {
            isWallSliding= false;
            anim.SetBool("IsWallSliding", false);
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
}
