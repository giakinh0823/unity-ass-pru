using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private SlidingController slidingController;

    private Timer timer;

    private bool isHiting;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        anim = GetComponent<Animator>();
        slidingController = gameObject.GetComponent<SlidingController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isHiting = true;
            anim.SetBool("isHit", true);
            timer.Duration = 2;
            timer.Run();
        }


        if (slidingController.isWallSliding || timer.Finished)
        {
            isHiting = false;
        }   
        
        if(!isHiting)
        {
            anim.SetBool("isHit", false);
        }
    }
}
