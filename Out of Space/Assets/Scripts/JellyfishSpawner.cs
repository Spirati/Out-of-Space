using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class JellyfishSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 5.0f;
    [SerializeField] private float spawnDelay = 5.0f;
    [SerializeField] private GameObject[] spawnObjects;
    
    public float spawnCountdown;
    public Collider2D spawnerCollider;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCountdown > 0) spawnCountdown -= Time.deltaTime;
        else Spawn();
    }

    private void OnDrawGizmos()
    {
        Vector2 position = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 position = transform.position;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(position, spawnRadius);
    }

    private void Spawn()
    {
        float angle = 180 * Random.value + 180;
        float radius = spawnRadius * Random.value;
        Vector2 spawnRay = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        
        RaycastHit2D[] castResults = new RaycastHit2D[] { };
        spawnerCollider.Raycast(spawnRay, castResults, radius);

        if (castResults.Length > 0)
        {
            radius = castResults.Min(result => result.distance);
        }
        
        Vector3 prefabPosition = transform.position + radius * (Vector3)spawnRay;
        GameObject spawnObject = spawnObjects[Random.Range((int) 0, (int) spawnObjects.Length)];
        GameObject newEnemy = Instantiate(spawnObject, prefabPosition, Quaternion.identity, transform);


        spawnCountdown = spawnDelay;
    }
}
