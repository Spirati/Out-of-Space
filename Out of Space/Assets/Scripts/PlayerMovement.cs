using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int angleOffset;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] private float thrust = 20.0f;
    [SerializeField] private ParticleSystem smokeSystem;
    [SerializeField] private Camera mainCamera;

    private Vector3 mouseDirection;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mouseDirection = -(Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position));
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg + angleOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Fire1") > 0 && playerRigidbody.gravityScale == 0)
        {
            smokeSystem.Play();
            float eulerZ = transform.rotation.eulerAngles.z;
            Vector3 direction = thrust*new Vector3(Mathf.Cos(eulerZ*Mathf.Deg2Rad), Mathf.Sin(eulerZ*Mathf.Deg2Rad), 0);
            playerRigidbody.AddForce(direction);
        }
        else
        {
            smokeSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if (transform.position.y <= -8) SceneManager.LoadScene("GameOver");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            transform.localPosition -= 2 * Vector3.forward;
            playerRigidbody.gravityScale = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            transform.localPosition -= 2 * Vector3.forward;
            playerRigidbody.gravityScale = 2;
        }
    }
}
