using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float Size = 0.5f;
    public float MinSize = 0.2f;
    public float MaxSize = 0.8f;
    public float Speed = 3.0f;
    public float LifeTime = 20.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.Size;
        _rigidbody.mass = this.Size;
        Destroy(gameObject, LifeTime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        this._rigidbody.AddForce(direction * this.Speed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag ==  "EnemyBullet")
        {
            if ((Size / 2) > MinSize)
            {
                Double();
                Double();
            }

            FindObjectOfType<AudioManager>().Play("AsteroidDestroyed");
            Destroy(this.gameObject);
            FindObjectOfType<GameManagerScript>().PlayerScore(this);
        }
    }

    private void Double()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidScript doubled = Instantiate(this, position, this.transform.rotation);
        doubled.Size = Size * 0.5f;
        doubled.SetTrajectory(Random.insideUnitCircle.normalized * Speed);
    }
}
