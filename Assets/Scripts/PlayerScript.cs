using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _accelerate;
    private float _turning;
    public float ThrustSpeed = 100.0f;
    public float TurnSpeed = 1.0f;
    public BulletScript Bullet;
    public Sprite EngineOn;
    public Sprite EngineOff;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _accelerate = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _accelerate ? EngineOn : EngineOff;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            _turning = 1.0f;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            _turning = -1.0f;
        else
            _turning = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void FixedUpdate()
    {
        if (_accelerate)
        {
            FindObjectOfType<AudioManager>().Play("EngineSound");
            _rigidbody.AddForce(this.transform.up * ThrustSpeed);
        }

        if (_turning != 0.0f)
            _rigidbody.AddTorque(_turning * this.TurnSpeed);
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("PlayerShoot");
        BulletScript bullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "EnemyBullet")
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManagerScript>().PlayerDied(this);
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }
    }
}
