using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    private class TimeEvent
    {
        public float TimeToExecute;
        public Callback Method;
    }

    public delegate void Callback();

    private List<TimeEvent> events;

    private void Awake()
    {
        events = new List<TimeEvent>();
    }

    public void Add(Callback method, float inSeconds)
    {
        events.Add(new TimeEvent{
            Method = method,
            TimeToExecute = Time.time + inSeconds
        });
    }

    private void Update()
    {
        if (events.Count == 0)
            return;

        for (int i =0; i < events.Count; i++)
        {
            var timeEvent = events[i];
            if(timeEvent.TimeToExecute <= Time.time)
            {
                timeEvent.Method();
                events.Remove(timeEvent);
            }
        }
    }

}
