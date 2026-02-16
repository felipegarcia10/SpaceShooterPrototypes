using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement2D : ShipMovement
{
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private float _improvedFireRate = 0.1f;
    [SerializeField] private float _improvedFireRateTime = 0.1f;
    private float _nextFireTime = 0f;
    private float pivotFireRate;

    protected override void Start()
    {
        base.Start();
        _nextFireTime = Time.time;
        pivotFireRate = _fireRate;
    }
    void Update()
    {
        transform.Translate(new Vector3(_moveVector.x, 0, _moveVector.y) * moveSpeed * Time.deltaTime);

        if (Time.time - _nextFireTime > _fireRate)
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
            _nextFireTime = Time.time;
        }
        

        //HandleMovement();
    }

    protected override void ClampPosition()
    {
        float yDistance = Mathf.Abs(mainCamera.transform.position.y - fixedY);

        // Bottom-left and top-right corners of the screen in world space
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, yDistance)
        );

        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, yDistance)
        );

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x,
            bottomLeft.x + padding,
            topRight.x - padding
        );

        pos.z = Mathf.Clamp(pos.z,
            bottomLeft.z + padding,
            topRight.z - padding
        );

        pos.y = fixedY; // 🔒 lock Y

        transform.position = pos;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("FireRate"))
        {
            StartCoroutine(HandleFireRatePowerUp());
            //_fireRate = pivotFireRate;
        }
    }

    private IEnumerator HandleFireRatePowerUp()
    {

        _fireRate = _improvedFireRate;
        yield return new WaitForSeconds(_improvedFireRateTime);
        _fireRate = pivotFireRate;

    }

    public override void OnAttack(InputValue value)
    {
        // Additional 2D-specific attack behavior can be added here
    }

}
