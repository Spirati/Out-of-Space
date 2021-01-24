using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class JellyfishMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private Rigidbody2D rb;
    public Rigidbody2D playerrb;
    public Vector2 direction;
    public GameObject[] pickups;
    
    public float pickupChance = 4;
    private Vector2 deathTarget;
    public float frozenTime = 0;

    private enum JellyfishState
    {
        Live,
        Dead,
        Frozen
    }
    

    private JellyfishState state = JellyfishState.Live;


    // Start is called before the first frame update
    void Start()
    {
        playerrb = GameObject.Find("Player").gameObject.GetComponent<Rigidbody2D>();
        ResetDirection(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            ResetDirection(speed * Time.deltaTime);
        }

        if (transform.localScale.z < 0.4 )
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (state == JellyfishState.Live)
        {
            rb.MovePosition(direction);
        }
        else if (state == JellyfishState.Frozen)
        {
            frozenTime -= Time.fixedDeltaTime;
            if (frozenTime <= 0) state = JellyfishState.Live;
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
        bool canSpawn = other.GetComponent<ClockPickup>() ? ClockPickup.CanSpawn() : SnowflakePickup.CanSpawn();
        if (Random.value > 1 / pickupChance && canSpawn)
        {
            GameObject pickup = pickups[Random.Range((int) 0, (int) pickups.Length)];
            Instantiate(pickup, transform.position, Quaternion.identity);
        }
        state = JellyfishState.Dead;
        deathTarget = playerrb.position;
        
        ParticleSystem.CollisionModule coll = other.GetComponent<ParticleSystem>().collision;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void ResetDirection(float step)
    {
        float deviation = Mathf.Deg2Rad * (20 * Random.value - 10); // deviate -10 to 10 degrees
        direction = Vector2.MoveTowards(rb.position, playerrb.position + new Vector2(Mathf.Cos(deviation), Mathf.Sin(deviation)), step);
    }
    
    public void Freeze(float seconds)
    {
        state = JellyfishState.Frozen;
        frozenTime = seconds;
    }

}
