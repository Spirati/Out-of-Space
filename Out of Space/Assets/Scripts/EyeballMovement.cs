using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class EyeballMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Rigidbody2D playerrb;
    public Vector2 direction;
    public GameObject[] pickups;
    public GameObject projectilePrefab;
    
    public float pickupChance = 4;
    private Vector2 deathTarget;
    public float shotDelay;
    public float frozenTime = 0.0f;
    
    

    private enum EyeballState
    {
        Live,
        Dead,
        Frozen
    }
    

    private EyeballState state = EyeballState.Live;

    void Start()
    {
        direction = Vector2.down;
        playerrb = GameObject.Find("Player").gameObject.GetComponent<Rigidbody2D>();
        shotDelay = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.z < 0.4 )
        {
            Destroy(gameObject);
        }

        if (state == EyeballState.Live)
        {
            if (shotDelay > 0) shotDelay -= Time.deltaTime;
            if (shotDelay <= 0)
            {
                shotDelay = 2 + Random.value;
                SpawnProjectile();
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (state == EyeballState.Dead)
        {
            rb.AddForce(Vector2.MoveTowards(rb.position, deathTarget, -10));
            transform.Rotate(new Vector3(0,0,10));
            transform.localScale *= 0.99f;
        }
        else if (state == EyeballState.Frozen)
        {
            frozenTime -= Time.fixedDeltaTime;
            if (frozenTime <= 0) state = EyeballState.Live;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (Random.value > 1 / pickupChance && ClockPickup.CanSpawn())
        {
            GameObject pickup = pickups[Random.Range((int) 0, (int) pickups.Length)];
            Instantiate(pickup, transform.position, Quaternion.identity);
        }
        state = EyeballState.Dead;
        deathTarget = playerrb.position;
        
        ParticleSystem.CollisionModule coll = other.GetComponent<ParticleSystem>().collision;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        direction *= -1;
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform);
    }
    
    public void Freeze(float seconds)
    {
        state = EyeballState.Frozen;
        frozenTime = seconds;
    }
}