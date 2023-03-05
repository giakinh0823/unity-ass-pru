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

    private JoystickController joystickController;

    void Start()
    {
        joystickController = GetComponent<JoystickController>();
        gunSprite = Instantiate(gunSprite, gun.transform.position, gun.transform.rotation);
        gunSprite.SetActive(false);
    }

    void FixedUpdate()
    {
        horizontal = joystickController.GetHorizontalValue();
        Vector2 joystickValue = joystickController.joystickValue;
        float angle = Mathf.Atan2(joystickValue.y, joystickValue.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        Quaternion lookRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
