using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clock : MonoBehaviour {

    [Header("Time")]
    public float timePerHour;
    public class HourEvent : UnityEvent<int> { }
    public HourEvent hourChanged = new HourEvent();
    int currentHour = -1;
    float time;

    [Header("Display")]

    [SerializeField]
    RectTransform armMinutes;

    [SerializeField]
    RectTransform armHours;

    public static Clock Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        //Increase the time based on the time speed
        time += Time.deltaTime/timePerHour;

        //Check if we reached a new hour
        int hour = Mathf.FloorToInt(time);
        if(hour>currentHour)
        {
            currentHour = hour;
            hourChanged.Invoke(hour % 24);
        }

        //Display the rotation of the clock arms
        armMinutes.localEulerAngles = Vector3.forward * -360 * (time % 1);
        armHours.localEulerAngles = Vector3.forward * -360 * (Mathf.Floor(time)/12 % 1);
    }
}
