using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePositionListener : MonoBehaviour 
{
    private List<TransferTrajectoryData> Positions;
    //public CatlikeBezierSpline BezierSpline;
    private long GlobalTime
    {
        get
        {
            return _globalTime;
        }
        set
        {
            _globalTime = value;
//            InitBezier();

            ChangeGlobalTime();
        }
    }

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private long _globalTime;
    //Need update by listner
    void OnValidate()
    {
        if(Application.isPlaying)
        {
            GlobalTime = _globalTime;
        }
    }

    public virtual void SetPosition(List<TransferTrajectoryData> data)
    {
        Positions = data;
//        InitBezier();
    }

    public void OnTimeTick(long current)
    {
        GlobalTime = current;
    }

    private void ChangeGlobalTime()
    {
        TransferTrajectoryData prevPoint;
        TransferTrajectoryData nextPoint;
        int index;
        FindCurrentPositions(out prevPoint, out nextPoint, out index);
        if(nextPoint == null)
        {
            transform.localPosition = prevPoint.Position;
        }
        else
        {
            //float toDestinationLocalTime = nextPoint.UTCTime - prevPoint.UTCTime;
            //float currentLocalTime = GlobalTime - prevPoint.UTCTime;
            float currntTimeLine = Mathf.InverseLerp(nextPoint.UTCTime, prevPoint.UTCTime, GlobalTime); // currentLocalTime / toDestinationLocalTime;
            transform.localPosition = Vector3.Lerp(prevPoint.Position, nextPoint.Position, currntTimeLine)/SpaceTimeParrametrs.Scale;
//            Debug.LogError(currntTimeLine);
//            currntTimeLine = currntTimeLine / Positions.Length;
//            transform.position = BezierSpline.GetPoint(currntTimeLine);
        }
    }

    private void FindCurrentPositions(out TransferTrajectoryData prevPoint, out TransferTrajectoryData nextPoint, out int index)
    {
        prevPoint = null;
        nextPoint = null;
        index = 0;
        for (int i = 0; i < Positions.Count; i++)
        {
            if(Positions[i].UTCTime > GlobalTime)
            {
                nextPoint = Positions[i];
                if(i != 0)
                {
                    index = i - 1;
                    prevPoint = Positions[index];

                }
                break;
            }
        }
        if(nextPoint == null)
        {
            index = Positions.Count - 1;
            prevPoint = Positions[index];
        }
    }

    private bool BezierInitied;

//    private void InitBezier()
//    {
//        if(BezierInitied)
//        {
//            return;
//        }
//        Debug.LogError("InitBezier");
//        BezierInitied = true;
//        BezierSpline.points = new Vector3[Positions.Length * 3 - 2];
//        BezierSpline.modes = new CatlikeBezierControlPointMode[(int)Mathf.Ceil((BezierSpline.points.Length + 1)/3f)];
//
//        for (int i = 0; i < Positions.Length; i++)
//        {
//            BezierSpline.SetControlPoint(i * 3, Positions[i].Position);
//            if(i != 0 && i)
//            {
//                BezierSpline.points[i * 3 - 1] = Positions[i - 1].Position;
//            }
//
//        }
//    }
}

