using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public float secondsElapsed;
    // Start is called before the first frame update
    void Start()
    {
        secondsElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        secondsElapsed += Time.deltaTime;
        int minutes = (int) (secondsElapsed / 60);
        string minutesString = (minutes < 10 ? "0" : "") + minutes.ToString();
        int seconds = (int) (secondsElapsed - 60 * minutes);
        string secondsString = (seconds < 10 ? "0" : "") + seconds.ToString();
        textObject.SetText("TIME: " + minutesString + ":" + secondsString);
    }

    static string FromSeconds(float s)
    {
        int minutes = (int) (s / 60);
        string minutesString = (minutes < 10 ? "0" : "") + minutes.ToString();
        int seconds = (int) (s - 60 * minutes);
        string secondsString = (seconds < 10 ? "0" : "") + seconds.ToString();
        return minutesString + ":" + secondsString;
    }
}
