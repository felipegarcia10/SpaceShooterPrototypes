using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject shield;
    GameObject bulletClone;

    private Vector2 _moveVector;

    [Header("Boundaries padding")]
    [SerializeField] private float padding = 0.5f;

    private Camera mainCamera;
    private float fixedY;

    void Start()
    {
        mainCamera = Camera.main;
        fixedY = transform.position.y;
    }

    void Update()
    {
        transform.Translate(new Vector3(_moveVector.x, 0, 0) * moveSpeed * Time.deltaTime);

        //HandleMovement();
    }

    private void LateUpdate()
    {
        ClampX();
    }

    private void ClampX()
    {
        // Distance from camera along its forward axis (Y)
        float yDistance = Mathf.Abs(mainCamera.transform.position.y - fixedY);

        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0.5f, yDistance)
        );

        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 0.5f, yDistance)
        );

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftEdge.x + padding, rightEdge.x - padding);
        pos.y = fixedY; // 🔒 ensure no vertical drift

        transform.position = pos;
    }
    public void OnMove(InputValue value)
    {
        // Read the input value as a Vector2 (x, y)
        _moveVector = value.Get<Vector2>();
    }

    public void OnAttack(InputValue value)
    {
        bulletClone = Instantiate(bullet, this.transform.position, this.transform.rotation);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            shield.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Asteroid"))
        {
            HUDManager.instance.OnShipDestroyed();
            gameObject.SetActive(false);
            
        }
    }
    

}
