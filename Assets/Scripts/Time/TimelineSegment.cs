using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineSegment : MonoBehaviour
{
    [SerializeField]
    private float SliderWidth;
    [SerializeField]
    private Text Text;

    public void SetData(TimelineSegmentData data)
    {
        RectTransform rTransform = transform as RectTransform;
        rTransform.localPosition = new Vector3((data.Position - .5f) * (SliderWidth - 22), 1);

        Text.text = data.Text;
    }
}

[System.Serializable]
public struct TimelineSegmentData
{
    public float Position;
    public string Text;
}