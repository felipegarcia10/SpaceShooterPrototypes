using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.transform.forward * speed * Time.deltaTime);
    }
}
