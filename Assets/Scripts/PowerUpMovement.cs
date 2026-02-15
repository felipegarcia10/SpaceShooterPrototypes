using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.Translate(-this.transform.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
