using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameState;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAbmientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    [SerializeField]
    public GameObject playerPrefab;

    public DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;



    private bool IsNight;

    private bool IsMined;

    ResourceController resourceController = new ResourceController();

    public delegate void ChangeToNightTime();
    public static event ChangeToNightTime OnChangeToNightTime;

/*    public delegate void RespawnResources();
    public static event RespawnResources Resources;*/

    RespawnResources respawnResources = new RespawnResources();




    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        respawnResources.cT = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        IsNight = true;
        IsMined = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        IsNightTime();

    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

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
            RenderSettings.fog = false;
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double precentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;


            sunLightRotation = Mathf.Lerp(180, 360, (float)precentage);
            
            RenderSettings.fogDensity = Mathf.Lerp(0, 0.05f, (float)precentage);


            RenderSettings.fog = true;

            OnChangeToNightTime?.Invoke();

        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }


    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAbmientLight, lightChangeCurve.Evaluate(dotProduct));

    }

    private void IsNightTime()
    {

        if ((currentTime.TimeOfDay < sunriseTime || currentTime.TimeOfDay > sunsetTime) && IsNight)
        {
            IsNight = false;
            RenderSettings.fogDensity = 0.001f;

            RenderSettings.fog = true;
            OnChangeToNightTime?.Invoke();
            RenderSettings.ambientIntensity = 0f;

        }
        
    }

}
