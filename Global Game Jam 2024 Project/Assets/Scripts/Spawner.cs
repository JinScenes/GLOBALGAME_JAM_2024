using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] lootPrefab;
    [SerializeField] private int xPos = 0;
    [SerializeField] private int zPos = 0;
    [SerializeField] public int totalLoots = 6;

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

        while (spawnedLootPositions.Count < totalLoots)
        {
            GameObject lootToSpawn = lootPrefab[0];

            bool positionValid = false;
            Vector3 spawnPosition = Vector3.zero;

            // Try to find a valid position for loot
            while (!positionValid)
            {

                int randomXpos = Random.Range(-xPos, xPos) + (int)transform.position.x;
                int randomZpos = Random.Range(-zPos, zPos) + (int)transform.position.z;

                spawnPosition = new Vector3(randomXpos, 0.5f, randomZpos);

                // Check distance with existing loot positions
                positionValid = IsPositionValid(spawnPosition);
            }

            // Instantiate loot at the valid position
            Instantiate(lootToSpawn, spawnPosition, Quaternion.identity);
            spawnedLootPositions.Add(spawnPosition);
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
}
