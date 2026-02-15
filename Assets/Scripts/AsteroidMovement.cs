using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int spawnProbability = 10;
    public GameObject powerUp;
    private int _randpowerUpSpawn;

    void Update()
    {
        transform.Translate(-this.transform.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandlePowerUpSpawn();
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void HandlePowerUpSpawn()
    {
        _randpowerUpSpawn = Random.Range(0, spawnProbability);


        if (_randpowerUpSpawn == spawnProbability - 1)
        {
            Instantiate(powerUp, this.transform.position, Quaternion.identity);
        }
    }


}
