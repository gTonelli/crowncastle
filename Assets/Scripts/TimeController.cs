using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    private DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");

        }
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTIme)
    {
        TimeSpan difference = toTIme - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    private void RotateSun()
    {
        float sunLightRotation;
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double precentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)precentage);
        } else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime,sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double precentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;


            sunLightRotation = Mathf.Lerp(180, 360, (float)precentage);

        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }
}
