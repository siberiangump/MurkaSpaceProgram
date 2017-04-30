using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionListener : MonoBehaviour 
{
    List<TransferTrajectoryData> Positions;
    public CatlikeBezierSpline BezierSpline;
    public LineRenderer TrajectoryRenderer;

    private double GlobalTime
    {
        get
        {
            return _globalTime;
        }
        set
        {
            _globalTime = value;
            InitBezier();

            ChangeGlobalTime();
        }
    }

    //[SerializeField]
    //[Range(0.0f, 30.0f)]
    private double _globalTime;
    //Need update by listner
    //void OnValidate()
    //{
    //    if(Application.isPlaying)Position
    //    {
    //        GlobalTime = _globalTime;
    //    }
    //}

    public virtual void SetPosition(List<TransferTrajectoryData> data)
    {
        Positions = data;
        InitBezier();
    }

    public void OnTimeTick(double current)
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
            float oneTimeSize = 1f / (Positions.Count - 1);
            float localLerpTime = (float)((GlobalTime - prevPoint.UTCTime) / (nextPoint.UTCTime - prevPoint.UTCTime));
            float currntTimeLine = Mathf.Lerp(index * oneTimeSize, (index + 1) * oneTimeSize, localLerpTime);
            //float currntTimeLine = Mathf.InverseLerp(nextPoint.UTCTime, prevPoint.UTCTime, GlobalTime); // currentLocalTime / toDestinationLocalTime;
            transform.localPosition = BezierSpline.GetPoint(currntTimeLine);///SpaceTimeParrametrs.Scale;
            transform.LookAt(transform.localPosition + BezierSpline.GetDirection(currntTimeLine));
        }
        if(TrajectoryRenderer != null)
        {
            Vector3[] positions = new Vector3[index + 1];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = Positions[i].Position;
            }
            positions[positions.Length - 1] = transform.localPosition;
            TrajectoryRenderer.positionCount = positions.Length;
            TrajectoryRenderer.SetPositions(positions);
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

    private void InitBezier()
    {
        if(BezierSpline==null)
        {
            BezierSpline = new GameObject().AddComponent<CatlikeBezierSpline>();
            BezierSpline.transform.parent = null;
            BezierSpline.transform.position = Vector3.zero;
        }
        if(BezierInitied)
        {
            return;
        }
        Debug.LogError("InitBezier");
        BezierInitied = true;
        BezierSpline.points = new Vector3[Positions.Count * 3 - 2];
        BezierSpline.modes = new CatlikeBezierControlPointMode[(int)Mathf.Ceil((BezierSpline.points.Length + 1)/3f)];

        for (int i = 0; i < Positions.Count; i++)
        {
            BezierSpline.SetControlPoint(i * 3, Positions[i].Position);
            Vector3 currenControllPointPos = Positions[i].Position;

            if(i != 0)
            {
                Vector3 preDirPos = BezierSpline.points[(i-1) * 3 + 1];
                BezierSpline.points[i * 3 - 1] = Vector3.Lerp(preDirPos, currenControllPointPos, 0.66f);
            }
            if(i != 0 && i != Positions.Count - 1)
            {
                Vector3 toNextCPDistance = Positions[i+1].Position - currenControllPointPos;
                Vector3 prev = (currenControllPointPos - BezierSpline.points[i * 3 - 1]).normalized;
                BezierSpline.points[i * 3 + 1] = currenControllPointPos +( prev * (toNextCPDistance.magnitude / 3));
            }
        }
    }
}