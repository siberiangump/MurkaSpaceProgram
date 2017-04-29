using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSegmentsController : MonoBehaviour
{
    [SerializeField]
    private TimelineSegmentData[] Segments;
    [SerializeField]
    private GameObject SegmentPrefab;
    [SerializeField]
    private Transform SegmentsHolder;

    public void Initialize(GlobalTimeData data)
    {
        while (SegmentsHolder.childCount > 0)
            DestroyImmediate(SegmentsHolder.GetChild(0).gameObject);

        Segments = new TimelineSegmentData[(int)((data.EndTime - data.StartTime) / data.Step) + 1];
        for (int i = 0; i < Segments.Length; i++)
        {
            Segments[i] = new TimelineSegmentData()
            {
                Position = (float)i / (Segments.Length - 1),
                Text = ""
            };
        }
        Segments[0].Text = data.StartTime.ToString("F2");
        Segments[Segments.Length - 1].Text = data.EndTime.ToString("F2");

        foreach (var segment in Segments)
        {
            var go = Instantiate<GameObject>(SegmentPrefab);
            go.transform.parent = SegmentsHolder;
            go.GetComponent<TimelineSegment>().SetData(segment);
        }
    }
}
