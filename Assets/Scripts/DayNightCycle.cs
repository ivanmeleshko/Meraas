using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{

    public float time;
    public TimeSpan currentTime;
    public Transform SunTransform;
    public Light Sun;
    public Text timetext;
    public int days;

    public float intensity;
    public Color fogday = Color.gray;
    public Color fognight = Color.black;

    public int speed;


    // Update is called once per frame
    void Update()
    {
        ChangeTime(); 
    }


    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if (time > 86400)
        {
            days += 1;
            time = 0;
        }
        currentTime = TimeSpan.FromSeconds(time);
        string[] temptime = currentTime.ToString().Split(":"[0]);
        timetext.text = temptime[0] + ":" + temptime[1];

        SunTransform.rotation = Quaternion.Euler(new Vector3(0, (time - 21600) / 86400 * 360, 0));

        if (time < 43200)
        {
            intensity = 1 - (43200 - time) / 43200;

        }
        else
        {
            intensity = 1 - (43200 - time) / 43200 * -1;
        }
        RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity * intensity);
        Sun.intensity = intensity;
    }
}
