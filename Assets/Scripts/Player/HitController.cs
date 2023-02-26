using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SlidingController slidingController;

    private int hit;


    void Start()
    {
        hit = 0;
        anim = GetComponent<Animator>();
        slidingController = gameObject.GetComponent<SlidingController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (hit >= 2 && !slidingController.isWallSliding)
            {
                anim.SetTrigger("isLongHit");
                hit = 0;
            }
            else if(!slidingController.isWallSliding)
            {
                anim.SetTrigger("isShortHit");
                hit++;
            }
        }
    }
}
