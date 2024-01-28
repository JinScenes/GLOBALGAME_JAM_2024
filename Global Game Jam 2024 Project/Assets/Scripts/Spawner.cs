using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] lootPrefab;
    [SerializeField] private int xPos = 0;
    [SerializeField] private int zPos = 0;
    [SerializeField] public int totalLoots = 20;

    protected const float lootSpawnTime = 0.2f;
    protected const float minDistanceBetweenLoots = 5f;

    private List<Vector3> spawnedLootPositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(L_Spawner());
    }

    private IEnumerator L_Spawner()
    {
        yield return new WaitForSeconds(lootSpawnTime);

        for (int i = 0; i < lootPrefab.Length; i++)
        {
            GameObject lootToSpawn = lootPrefab[i];
            bool positionValid = false;
            Vector3 spawnPosition = Vector3.zero;

            // Try to find a valid position for loot
            while (!positionValid)
            {
                int randomXpos = Random.Range(-xPos, xPos) + (int)transform.position.x;
                int randomZpos = Random.Range(-zPos, zPos) + (int)transform.position.z;

                spawnPosition = new Vector3(randomXpos, -1.5f, randomZpos);

                // Check distance with existing loot positions
                positionValid = IsPositionValid(spawnPosition);
            }

            // Instantiate loot at the valid position
            Instantiate(lootToSpawn, spawnPosition, Quaternion.identity);
            spawnedLootPositions.Add(spawnPosition);

            // Ensure there's a delay between spawns for performance and visual effect
            yield return new WaitForSeconds(lootSpawnTime);
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in spawnedLootPositions)
        {
            float distance = Vector3.Distance(position, existingPosition);
            if (distance < minDistanceBetweenLoots)
            {
                return false;
            }
        }
        return true;
    }

    void OnDrawGizmos()
    {
        // Set the color of the Gizmos, making it semi-transparent
        Gizmos.color = new Color(0, 1, 0, 0.5f);

        // Draw a wireframe cube to represent the spawn area with Gizmos
        // The center of the cube is the position of this GameObject
        // The size of the cube is determined by xPos and zPos, doubled to represent full width and length
        Gizmos.DrawWireCube(transform.position, new Vector3(xPos * 2, 1, zPos * 2));
    }
}
