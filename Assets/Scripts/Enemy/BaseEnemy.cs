namespace Enemy
{
    using UnityEngine;

    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        protected Healbar healbar;

        [SerializeField]
        protected float currentHealth = 1f;

        protected Animator Animator;

        private readonly float dameArm   = 0.2f;
        private readonly float dameGun   = 0.25f;
        private readonly float dameKnife = 0.5f;

        public static readonly int HealthAnimKey = Animator.StringToHash("Health");

        protected virtual void Awake()
        {
            this.Animator = this.GetComponent<Animator>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ArmLeft")
                                                          || collision.gameObject.CompareTag("ArmRight"))
            {
                Debug.Log("Player");

                this.PlaySound();
                this.healbar.gameObject.SetActive(true);

                this.currentHealth -= this.dameArm;


                this.Animator.SetFloat(HealthAnimKey, this.currentHealth);
                if (this.currentHealth <= 0)
                {
                    this.currentHealth = 0;
                    Destroy(this.gameObject, 2f);
                }
            }
            else if (collision.gameObject.CompareTag("Knife"))
            {
                Debug.Log("Knife");
                this.PlaySound();
                this.healbar.gameObject.SetActive(true);

                this.currentHealth -= this.dameKnife;


                this.Animator.SetFloat(HealthAnimKey, this.currentHealth);

                if (this.currentHealth <= 0)
                {
                    this.currentHealth = 0;
                    Destroy(this.gameObject, 2f);
                }
            }
            else if (collision.gameObject.CompareTag("Bullet"))
            {
                Debug.Log("Gun");

                this.PlaySound();
                this.healbar.gameObject.SetActive(true);
                var rotation = collision.gameObject.transform.rotation;

                this.gameObject.transform.localScale = rotation.x * Vector3.right.x > 0 ? new Vector3(1.0369f, 0.9648f, 1) : new Vector3(-1.0369f, 0.9648f, 1);

                this.currentHealth -= this.dameGun;


                this.Animator.SetFloat(HealthAnimKey, this.currentHealth);

                if (this.currentHealth <= 0)
                {
                    this.currentHealth = 0;
                    Destroy(this.gameObject, 2f);
                }
            }
        }

        protected virtual void PlaySound()
        {
            this.gameObject.GetComponent<AudioSource>()?.Play();
        }
    }
}