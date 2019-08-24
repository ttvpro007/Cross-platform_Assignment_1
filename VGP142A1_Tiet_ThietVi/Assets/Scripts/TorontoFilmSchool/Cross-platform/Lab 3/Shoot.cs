using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Transform projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;

    void Start()
    {
        projectileSpawnPoint = transform.Find("Projectile_spawn_point").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PewPew(projectilePrefab);
        }
    }

    void PewPew(GameObject prefab)
    {
        Instantiate(prefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }
}
