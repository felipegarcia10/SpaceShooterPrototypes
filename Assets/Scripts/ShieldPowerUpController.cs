using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class ShieldPowerUpController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float enabledTime;
    
    void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
    private void OnEnable()
    {
        StartCoroutine(ShieldEnabledRoutine());
    }
    private IEnumerator ShieldEnabledRoutine()
    {
        while (true) // Infinite loop to keep spawning
        {
            yield return new WaitForSeconds(enabledTime);
            gameObject.SetActive(false);
        }
    }
}
