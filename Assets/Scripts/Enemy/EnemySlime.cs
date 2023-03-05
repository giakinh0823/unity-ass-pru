using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private Animator animator;
    private float damage = 0.05f;
    private float currentHealth = 1f;
    [SerializeField]
    private Healbar healbar;

    public float speed = 2.0f; 
    public Vector2 direction = Vector2.right;

    bool isWallTouch = false;
    public LayerMask wallerLayerMask;
    [SerializeField]
    private Transform wallCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Health", currentHealth);


    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.03f, 0.5f), 0, wallerLayerMask);
        if (isWallTouch)
        {
            Debug.Log("va cham vs wall");
            Flip();
            direction = Vector2.left;

        }

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healbar.gameObject.SetActive(true);

            Quaternion rotation = collision.gameObject.transform.rotation;
            gameObject.transform.rotation = rotation;
            currentHealth -= damage;
            Debug.Log(currentHealth);
            animator.SetFloat("Health", currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject, 3f);
            }
        }

    }


    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
