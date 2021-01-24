using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class SpikeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private Rigidbody2D rb;
    private Rigidbody2D playerrb;
    public Vector2 direction;
    public GameObject[] pickups;
    
    public float pickupChance = 4;
    private Vector2 deathTarget;
    public float frozenTime = 0.0f;
    

    private enum SpikeState
    {
        Live,
        Dead,
        Frozen
    }
    

    private SpikeState state = SpikeState.Live;

    void Start()
    {
        direction = Vector2.down;
        playerrb = GameObject.Find("Player").gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.z < 0.4 )
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (state == SpikeState.Live)
        {
            transform.position += Time.fixedDeltaTime*speed*(Vector3)direction;
        }
        else if (state == SpikeState.Frozen)
        {
            frozenTime -= Time.fixedDeltaTime;
            if (frozenTime <= 0) state = SpikeState.Live;
        }
        else
        {
            rb.AddForce(Vector2.MoveTowards(rb.position, deathTarget, -10));
            transform.Rotate(new Vector3(0,0,10));
            transform.localScale *= 0.99f;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (Random.value > 1 / pickupChance && ClockPickup.CanSpawn())
        {
            GameObject pickup = pickups[Random.Range((int) 0, (int) pickups.Length)];
            Instantiate(pickup, transform.position, Quaternion.identity);
        }
        state = SpikeState.Dead;
        deathTarget = playerrb.position;
        
        ParticleSystem.CollisionModule coll = other.GetComponent<ParticleSystem>().collision;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        direction *= -1;
    }

    public void Freeze(float seconds)
    {
        state = SpikeState.Frozen;
        frozenTime = seconds;
    }
}
