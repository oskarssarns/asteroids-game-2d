using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public AsteroidScript asteroidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;

    public void Start()
    {
        InvokeRepeating(nameof(Spawn),spawnRate, spawnAmount);
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(trajectoryVariance * -1, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            AsteroidScript asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);
            asteroid.SetTrajectory(rotation * (spawnDirection * -1));
        }
    }
}
