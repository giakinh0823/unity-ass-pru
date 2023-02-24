using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private SlidingController slidingController;

    private bool isHiting;

    void Start()
    {
        anim = GetComponent<Animator>();
        slidingController = gameObject.GetComponent<SlidingController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isHiting = true;
            anim.SetBool("isHit", true);
        }

        if (slidingController.isWallSliding)
        {
            isHiting = false;
        }
        
        if(!isHiting)
        {
            anim.SetBool("isHit", false);
        }
    }
}
