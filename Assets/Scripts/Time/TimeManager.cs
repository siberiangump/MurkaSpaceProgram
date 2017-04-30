using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public bool IsPaused { get; set; }

    [SerializeField] private float TimeflowSpeed;
    [SerializeField] private GlobalTimeData TimeData;
    [SerializeField] private TimelineSegmentsController Segments;
    [SerializeField] private Slider Slider;
    [SerializeField] private InputField SpeedInput;
    [SerializeField] private InputField PositionInput;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button PauseButton;

    private Action<float> OnTick;
    private Action<double> OnTickUTC;
    private TimelineDragListener DragListener;
    private TimelineInputListener InputListener;

    //private float TimeValue
    //{
    //    get
    //    {
    //        r
    //    }
    //}

    private double UTCValue
    {
        get
        {
            return TimeData.StartTime + (double)((TimeData.EndTime - TimeData.StartTime)*Slider.value);
        }
    }

    void Start()
    {
        TimeData.StartTime = long.MaxValue;
        TimeData.EndTime = long.MinValue;

        SetTimeData(TimeData);

        IsPaused = true;
        SpeedInput.text = TimeflowSpeed.ToString();
        SpeedInput.onValueChanged.AddListener(SetTimeflowSpeed);
        PositionInput.onValueChanged.AddListener(SetTime);
        PlayButton.onClick.AddListener(() => IsPaused = false);
        PauseButton.onClick.AddListener(() => IsPaused = true);
        Slider.onValueChanged.AddListener((x) => PositionInput.text = x.ToString("F2"));
        DragListener = Slider.GetComponent<TimelineDragListener>();
        InputListener = PositionInput.GetComponent<TimelineInputListener>();
    }

    void Update()
    {
        if (!IsPaused && !DragListener.IsDragging && !InputListener.IsEditing)
            Slider.value += Time.deltaTime * TimeflowSpeed;
             
        if (OnTickUTC != null)
            OnTickUTC(UTCValue);
    }
    
    public void SubscribeOnTick(Action<double> func)
    {
        OnTickUTC += func;
    }

    public void UnsubscribeOnTick(Action<double> func)
    {
        OnTickUTC -= func;
    }

    public void SetTimeData(GlobalTimeData data)
    {
        TimeData = data;
        Segments.Initialize(TimeData);
        Slider.minValue = 0;
        Slider.maxValue = 1;
    }

    public void SetStart(long startTime)
    {
        TimeData.StartTime = TimeData.StartTime > startTime ? startTime : TimeData.StartTime;
        Segments.Initialize(TimeData);
    }
    
    public void SetEnd(long endTime)
    {
        TimeData.EndTime = TimeData.EndTime < endTime ? endTime : TimeData.EndTime;
        Segments.Initialize(TimeData);
    }

    public void SetTimeflowSpeed(string input)
    {
        float speed;
        if (float.TryParse(input, out speed))
        {
            TimeflowSpeed = speed;
        }
    }

    public void SetTime(string input)
    {
        long current;
        if (long.TryParse(input, out current))
        {
            Slider.value = Mathf.InverseLerp(TimeData.StartTime, TimeData.EndTime, current);
        }
    }
}

[System.Serializable]
public struct GlobalTimeData
{
    public long StartTime;
    public long EndTime;
    public long Step;
}



