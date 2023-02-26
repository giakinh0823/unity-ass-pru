using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private SlidingController slidingController;
    [SerializeField]
    private PlayerController playerController;

    private int hit;


    void Start()
    {
        hit = 0;
        slidingController = gameObject.GetComponent<SlidingController>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (hit >= 2 && !slidingController.isWallSliding)
            {
                playerController.anim.SetTrigger("isLongHit");
                hit = 0;
            }
            else if(!slidingController.isWallSliding)
            {
                playerController.anim.SetTrigger("isShortHit");
                hit++;
            }
        }
    }
}
