using System.Collections;
using UnityEngine;

public class AsteroidSpawnerController : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab to spawn
    public Vector3 spawnAreaSize = new Vector3(10f, 10f, 10f); // Size of the spawn area (if not using a collider)
    public float spawnInterval = 2f; // Time between spawns

    // Optional: Use a BoxCollider to visually define the area in the editor
    private BoxCollider spawnCollider;
    public Vector3 desiredScale;
    private float randomScale;

    void Start()
    {
        spawnCollider = GetComponent<BoxCollider>();
        if (spawnCollider != null)
        {
            // Disable the collider's physics but keep its bounds information
            spawnCollider.isTrigger = true;
        }

        // Start the spawning process
        StartCoroutine(SpawnObjectsRoutine());
    }

    private IEnumerator SpawnObjectsRoutine()
    {
        while (true) // Infinite loop to keep spawning
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPosition;

        if (spawnCollider != null)
        {
            // Get a random point within the collider's bounds
            Bounds bounds = spawnCollider.bounds;
            spawnPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        else
        {
            // Get a random point based on the public spawnAreaSize variable
            spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f),
                Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
            );
        }

        // Instantiate the prefab at the random position with no rotation
        randomScale = Random.Range(1f, 10f);
        desiredScale = new Vector3(
                randomScale,
                randomScale,
                randomScale
            );

        GameObject instantiatedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        instantiatedObject.transform.localScale = desiredScale;
    }


}
