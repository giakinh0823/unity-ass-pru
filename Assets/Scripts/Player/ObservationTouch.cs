using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationTouch : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    private int damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Observation")
        {
            playerController.TakeDamage(damage);
        }
    }
}
