using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 direction;

    private Rigidbody2D playerrb;
// Start is called before the first frame update
    void Start()
    {
        playerrb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(playerrb.transform.position.y - transform.position.y,
            playerrb.transform.position.x - transform.position.x);
        direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * direction;
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Enemy")) Destroy(gameObject);
    }
}
