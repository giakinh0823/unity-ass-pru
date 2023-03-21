using UnityEngine;

public class EnemyBird : BaseEnemy
{

    private Animator animator;
    private float damage = 0.05f;
    private float maxHealth = 1.5f;
    public float currentHealth = 1.5f;
    [SerializeField]
    private Healbar healbar;

    public float speed = 2.0f;
    public Vector3 direction;

    bool isWallTouch = false;
    public LayerMask wallerLayerMask;
    [SerializeField]
    private Transform wallCheckPoint;

    public Transform playerTransfrom;
    public bool isChasing;
    public float chaseDistance;

    public GameObject bulletPrefab;
    private float bulletSpeed = 15f;
    Vector3 closestWalkerDirection = Vector3.zero;
    TimerEnemy timers;
    TimerEnemy timers2;
    public float distanceLimit = 3f;
    private float distanceMoved = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);
        direction = Vector3.right;

        timers = GetComponent<TimerEnemy>();
        timers.alarmTime = 1;
        timers.StartTime();
        
        timers2 = GetComponent<TimerEnemy>();
        timers2.alarmTime = 1;
        timers2.StartTime();

    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransfrom = player.transform;
        }

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
                transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x > 0)
            {
                transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (gameObject.transform.position.x < playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (gameObject.transform.position.x > playerTransfrom.position.x && gameObject.transform.localScale.x * Vector3.right.x < 0)
            {
                transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
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
                direction = -direction;
            }


        }
        animator.SetFloat("Health", currentHealth);
        if (timers2.isFinish)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += currentHealth * 5 / 100;
                timers2.alarmTime = 1;
                timers2.StartTime();
            }
            else
            {
                healbar.gameObject.SetActive(false);
                return;
            }
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject, 2f);
        }
        healbar.localScale.x = currentHealth;



    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ArmLeft")
            || collision.gameObject.CompareTag("ArmRight"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
            }
            currentHealth -= GetDameArm();


        }
        else if (collision.gameObject.CompareTag("Knife"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
            }
            currentHealth -= GetDameKnife();



        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            healbar.gameObject.SetActive(true);
            Quaternion rotation = collision.gameObject.transform.rotation;
            if (rotation.x * Vector3.right.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1.0369f, 0.9648f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1.0369f, 0.9648f, 1);
            }
            currentHealth -= GetDameGun();



        }


    }

    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
        
}
