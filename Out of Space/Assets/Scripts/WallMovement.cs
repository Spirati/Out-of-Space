using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public enum WallDirection
    {
        Horizontal,
        Vertical
    };
    public float wallSpeed = 1;
    public WallDirection wallDirection;
    public bool moving = false;
    public float startDelay = 5.0f;
    public SpriteRenderer sprender;
    public TimerDisplay timerScript;

    public bool reversed = false;
    public float reverseClock = 0.0f;
    public float reverseMax = 3.0f;
    

    // Update is called once per frame
    void Update()
    {
        if (timerScript.secondsElapsed >= startDelay) moving = true;
    }

    void FixedUpdate()
    {
        if (!moving) return;
        int cofactor = reversed ? -1 : 1;
        switch (wallDirection)
        {
            case WallDirection.Horizontal:
                sprender.size += new Vector2(cofactor*wallSpeed, 0);
                if (sprender.size.x < 2) sprender.size = new Vector2(2, 12);
                //transform.localPosition += new Vector3(0, -wallSpeed, 0);
                
                break;
            case WallDirection.Vertical:
                sprender.size += new Vector2(cofactor*wallSpeed, -cofactor*wallSpeed);
                if (sprender.size.x < 2) sprender.size = new Vector2(2, 12);
                break;
        }

        if (reversed)
        {
            reverseClock -= Time.fixedDeltaTime;
            if (reverseClock <= 0)
            {
                reverseClock = 0;
                reversed = false;
            }
        }
    }

    public void Reverse()
    {
        if (!moving) return;
        reversed = true;
        reverseClock = reverseMax;
    }
}
