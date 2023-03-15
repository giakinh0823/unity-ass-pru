using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

public class EnemySlime : BaseEnemy
{
    public float   speed = 2.0f;
    public Vector3 direction;

    bool             isWallTouch = false;
    public LayerMask wallerLayerMask;

    [SerializeField]
    private Transform wallCheckPoint;

    public  Transform playerTransfrom;
    public  bool      isChasing;
    public  float     chaseDistance;
    public  float     distanceLimit = 3f;
    private float     distanceMoved = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.Animator.SetFloat(HealthAnimKey, currentHealth);
        direction = Vector3.right;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransfrom = player.transform;
        }

        healbar.localScale.x = currentHealth;
        if (playerTransfrom != null && Vector3.Distance(transform.position, playerTransfrom.position) <= chaseDistance)
        {
            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale =  new Vector3(-0.2511116f, 0.3103755f, 1);
                transform.position   += Vector3.left * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale =  new Vector3(0.2511116f, 0.3103755f, 1);
                transform.position   += Vector3.right * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale =  new Vector3(0.2511116f, 0.3103755f, 1);
                transform.position   += Vector3.right * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale =  new Vector3(-0.2511116f, 0.3103755f, 1);
                transform.position   += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-0.2511116f, 0.3103755f, 1);
            }
            else
            {
                transform.localScale = new Vector3(0.2511116f, 0.3103755f, 1);
            }

            distanceMoved += speed * Time.deltaTime;
            if (distanceMoved >= distanceLimit)
            {
                distanceMoved = 0f;
                direction     = -direction;
            }
        }
    }
}