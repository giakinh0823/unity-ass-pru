using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public Rigidbody2D rigidBody;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {

        float moveInput = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);

        if (moveInput != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

}
