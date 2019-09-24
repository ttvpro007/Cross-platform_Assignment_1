using UnityEngine;
using RPG.Attributes;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] collectiblePrefab;
    bool hasSpawned;

    private void Awake()
    {
        if (!spawnPoint) spawnPoint = GetComponent<Transform>();
    }

    private void Start()
    {

        if (collectiblePrefab.Length == 0)
        {
            Debug.LogError("No reference to collectible prefab");
        }

        hasSpawned = false;
    }

    private void Update()
    {
        if (GetComponent<Health>().IsDead() && !hasSpawned)
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