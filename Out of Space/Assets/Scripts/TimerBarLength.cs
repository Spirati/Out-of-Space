using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBarLength : MonoBehaviour
{
    private RectTransform bar;
    public WallMovement wallScript;

    void Start()
    {
        bar = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //bar.position = new Vector3(133.01f, -54.3f);
        float fraction = 166 * (wallScript.reverseClock / wallScript.reverseMax);
        bar.sizeDelta = new Vector2(fraction, 16+1/6);
        //bar.localPosition += new Vector3(Mathf.Clamp(fraction-166, 0, 166), 0, 0);
    }
}
