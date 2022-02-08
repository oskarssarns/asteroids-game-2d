using UnityEngine;

public class AlienBulletScript : MonoBehaviour
{
    private GameObject _target;
    private Rigidbody2D _bulletRigidBody;
    public float Speed;
    public float BulletLiveTime;

    private void Start()
    {
        _bulletRigidBody = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");

        if (_target != null)
        {
            Vector2 moveDir = (_target.transform.position - transform.position).normalized * Speed;
            _bulletRigidBody.AddTorque(360);
            _bulletRigidBody.velocity = new Vector2(moveDir.x, moveDir.y);
            Destroy(this.gameObject, BulletLiveTime);
        }

        StopShooting();
    }

    private void StopShooting()
    {
        if (_target == null)
            this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            _bulletRigidBody.velocity = Vector2.zero;
            _bulletRigidBody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 0);
        }
    }
}
