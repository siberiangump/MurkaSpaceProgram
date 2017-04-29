using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private GlobalTimeData TimeData;
    [SerializeField]
    private TimelineSegmentsController Segments;
    [SerializeField]
    private Slider Slider;

    private Action<float> OnTick;

    private float TimeValue
    {
        get
        {
            return Time.time;
        }
    }

    void Start()
    {
        SetTimeData(TimeData);
    }

    void Update()
    {
        if (OnTick != null)
            OnTick(Slider.value);
    }

    public void SubscribeOnTick(Action<float> func)
    {
        OnTick += func;
    }

    public void SetTimeData(GlobalTimeData data)
    {
        TimeData = data;
        Segments.Initialize(TimeData);
        Slider.minValue = data.StartTime;
        Slider.maxValue = data.EndTime;
    }
}

[System.Serializable]
public struct GlobalTimeData
{
    public float StartTime;
    public float EndTime;
    public float Step;
}
