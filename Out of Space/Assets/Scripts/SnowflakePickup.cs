using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowflakePickup : MonoBehaviour
{
    public float freezeTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.name.Equals("Player")) return;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<JellyfishMovement>())
            {
                enemy.GetComponent<JellyfishMovement>().Freeze(freezeTime);
            }
            else if (enemy.GetComponent<SpikeMovement>())
            {
                enemy.GetComponent<SpikeMovement>().Freeze(freezeTime);
            }
            else
            {
                enemy.GetComponent<EyeballMovement>().Freeze(freezeTime);
            }
        }

        GameObject.Find("EnemySpawner").GetComponent<JellyfishSpawner>().spawnCountdown += freezeTime;
        Destroy(gameObject);
    }

    public static bool CanSpawn()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }
}
