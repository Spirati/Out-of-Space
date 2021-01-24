using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPickup : MonoBehaviour
{
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
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            wall.GetComponent<WallMovement>().Reverse();
        }
        Destroy(gameObject);
    }

    public static bool CanSpawn()
    {
        return GameObject.FindGameObjectWithTag("Wall").GetComponent<WallMovement>().moving;
    }
}
