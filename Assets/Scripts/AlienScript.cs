using UnityEngine;

public class AlienScript : MonoBehaviour
{
    public float Speed;
    private Transform _player;
    public float LineOfSight;
    public float ShootingRange;
    public float FireRate = 1f;
    private float _nextFireTime;
    public GameObject Bullet;
    public GameObject BulletParent;
    private Rigidbody2D _rigidbody;
    public int AlienLives = 50;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if (distanceFromPlayer < LineOfSight && distanceFromPlayer > ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _player.position, Speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= ShootingRange && _nextFireTime < Time.time)
        {
            Instantiate(Bullet, BulletParent.transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("AlienShoot");
            _nextFireTime = Time.time + FireRate;
        }

        if (AlienLives <= 0)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0.0f;
            Destroy(gameObject);
            FindObjectOfType<GameManagerScript>().NextLevel();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSight);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            FindObjectOfType<GameManagerScript>().AlienHit(this);
            if (AlienLives <= 0)
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.angularVelocity = 0.0f;
                Destroy(gameObject);
            }
        }
    }
}
