using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform  shootPoint;
    public float      bulletSpeed;

    private void Start()
    {
        FindObjectOfType<InputManager>().attack += this.Shoot;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed);
    }
}