using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBarLength : MonoBehaviour
{
    private RectTransform bar;

    void Start()
    {
        bar = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        float freezeTime;
        if (!enemy) freezeTime = 0;
        else
        {
            if (enemy.GetComponent<JellyfishMovement>())
            {
                freezeTime = enemy.GetComponent<JellyfishMovement>().frozenTime;
            } else if (enemy.GetComponent<SpikeMovement>())
            {
                freezeTime = enemy.GetComponent<SpikeMovement>().frozenTime;
            }
            else
            {
                freezeTime = enemy.GetComponent<EyeballMovement>().frozenTime;
            }
        }
        //bar.position = new Vector3(133.01f, -54.3f);
        float fraction = 166 * (freezeTime / 2.0f);
        bar.sizeDelta = new Vector2(fraction, 16+1/6);
        //bar.localPosition += new Vector3(Mathf.Clamp(fraction-166, 0, 166), 0, 0);
    }
}
