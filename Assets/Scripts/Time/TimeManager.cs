using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public bool IsPaused { get; set; }

    [SerializeField]
    private float TimeflowSpeed;
    [SerializeField]
    private GlobalTimeData TimeData;
    [SerializeField]
    private TimelineSegmentsController Segments;
    [SerializeField]
    private Slider Slider;
    [SerializeField]
    private InputField SpeedInput;
    [SerializeField]
    private InputField PositionInput;
    [SerializeField]
    private Button PlayButton;
    [SerializeField]
    private Button PauseButton;

    private Action<float> OnTick;
    private TimelineDragListener DragListener;
    private TimelineInputListener InputListener;

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
        float time;
        if (float.TryParse(input, out time))
        {
            Slider.value = Mathf.Clamp(time, TimeData.StartTime, TimeData.EndTime);
        }
    }
}

[System.Serializable]
public struct GlobalTimeData
{
    public float StartTime;
    public float EndTime;
    public float Step;
}
