using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        transform.position += parallaxSpeed * Time.deltaTime * Vector3.down;
        if (transform.position.y <= -12) transform.localPosition += 24 * Vector3.up;
    }
}
