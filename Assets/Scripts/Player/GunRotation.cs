using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GunRotation : MonoBehaviour
{
    [SerializeField]
    public PlayerController playerController;
    [SerializeField]
    public MovementController movementController;
    [SerializeField]
    public GameObject gun;
    [SerializeField]
    public GameObject gunSprite;

    private float horizontal;
    public float angle;
    public float rotationSpeed = 50f;

    void Start()
    {
        gunSprite = Instantiate(gunSprite, gun.transform.position, gun.transform.rotation);
        gunSprite.SetActive(false);
    }

    void Update()
    {
        Joystick joystick = playerController.joystick;
        Debug.Log(joystick.SnapX + " "+ joystick.SnapY + " "+ joystick.DeadZone);
        horizontal = joystick.Horizontal;
        angle = Mathf.Atan2(joystick.Direction.y, joystick.Direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.Euler(0f, 0f, angle);
        gun.transform.rotation = lookRotation;


        if (gunSprite.activeSelf)
        {
            gunSprite.transform.position = gun.transform.position;   
            gunSprite.transform.rotation = lookRotation;
            gunSprite.GetComponent<SpriteRenderer>().transform.position = gunSprite.transform.position;
            if (horizontal < 0)
            {
                gunSprite.transform.localScale = new Vector3(1, -1, 1);
                gunSprite.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1, -1, 1);
            }
            else if (horizontal > 0)
            {
                gunSprite.transform.localScale = new Vector3(1, 1, 1);
                gunSprite.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (!movementController.isFaceRight && !movementController.isRuning)
        {
            gunSprite.transform.eulerAngles = new Vector3(180, 180, 0);
            gunSprite.GetComponent<SpriteRenderer>().transform.eulerAngles = new Vector3(180, 180, 0);
            gun.transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }
}
