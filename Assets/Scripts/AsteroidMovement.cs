using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int spawnProbability = 10;
    [SerializeField] public float lifetime = 5f;
    public GameObject shieldPowerUp;
    public GameObject fireRatedPowerUp;
    private int _randpowerUpSpawn;


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

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
            Instantiate(shieldPowerUp, this.transform.position, Quaternion.identity);
        }
        if (_randpowerUpSpawn == 0)
        {
            Instantiate(fireRatedPowerUp, this.transform.position, Quaternion.identity);
        }
    }


}
