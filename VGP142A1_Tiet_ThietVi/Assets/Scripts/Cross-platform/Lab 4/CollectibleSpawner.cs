using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] collectiblePrefab;
    bool hasSpawned;

    private void Start()
    {
        if (!spawnPoint) spawnPoint = GetComponent<Transform>();

        if (collectiblePrefab.Length == 0)
        {
            Debug.LogError("No reference to collectible prefab");
        }

        hasSpawned = false;
    }

    private void Update()
    {
        if (GetComponent<Health>().IsDead && !hasSpawned)
        {
            SpawnCollectible();
        }
    }

    private void SpawnCollectible()
    {
        int i = Random.Range(0, 2);

        Instantiate(collectiblePrefab[i], spawnPoint.position + Vector3.up, spawnPoint.rotation);
        hasSpawned = true;
    }
}