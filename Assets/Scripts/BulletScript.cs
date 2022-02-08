using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float BulletSpeed = 100.0f;
    public float MaxLifeTime = 5.0f;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.BulletSpeed);
        Destroy(this.gameObject, this.MaxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Alien")
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}