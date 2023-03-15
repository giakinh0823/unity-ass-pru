using Enemy;
using UnityEngine;

public class EnemyBird : BaseEnemy
{
    public float   speed = 2.0f;
    public Vector3 direction;

    public LayerMask wallerLayerMask;

    [SerializeField]
    private Transform wallCheckPoint;

    public Transform playerTransfrom;
    public bool      isChasing;
    public float     chaseDistance;

    public  GameObject bulletPrefab;
    private float      bulletSpeed            = 15f;
    Vector3            closestWalkerDirection = Vector3.zero;
    TimerEnemy         timers;
    public  float      distanceLimit = 3f;
    private float      distanceMoved = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.Animator.SetFloat("Health", currentHealth);
        direction = Vector3.right;

        timers           = GetComponent<TimerEnemy>();
        timers.alarmTime = 1;
        timers.StartTime();
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
            if (timers.isFinish)
            {
                closestWalkerDirection = (playerTransfrom.position - transform.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = closestWalkerDirection * bulletSpeed;
                bullet.GetComponent<AudioSource>().Play();
                Destroy(bullet, 4f);
                timers.alarmTime = 5;
                timers.StartTime();
            }

            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale =  new Vector3(-1.0369f, 0.9648f, 1);
                transform.position   += Vector3.left * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale =  new Vector3(1.0369f, 0.9648f, 1);
                transform.position   += Vector3.right * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale =  new Vector3(1.0369f, 0.9648f, 1);
                transform.position   += Vector3.right * speed * Time.deltaTime;
            }

            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale =  new Vector3(-1.0369f, 0.9648f, 1);
                transform.position   += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
            }

            distanceMoved += speed * Time.deltaTime;
            if (distanceMoved >= distanceLimit)
            {
                distanceMoved = 0f;
                direction     = -direction;
            }
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}