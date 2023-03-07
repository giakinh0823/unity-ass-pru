using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed;

    private MyPlayerActions playerInput;
    private InputAction attack;

    private void Awake()
    {
        playerInput = new MyPlayerActions();
    }


    void Update()
    {
        if (attack.triggered && gameObject.activeSelf)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed);
    }

    private void OnEnable()
    {
        attack = playerInput.Player.Attack;
        attack.Enable();
    }

    private void OnDisable()
    {
        attack = playerInput.Player.Attack;
        attack.Disable();
    }
}
