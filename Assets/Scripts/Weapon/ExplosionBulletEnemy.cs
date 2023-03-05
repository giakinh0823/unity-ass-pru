using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBulletEnemy : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}
