using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionListener : MonoBehaviour 
{
    public PositionData[] Positions;
    public CatlikeBezierSpline BezierSpline;


    private float GlobalTime
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

    [SerializeField]
    [Range(0.0f, 30.0f)]
    private float _globalTime;
    //Need update by listner
    void OnValidate()
    {
        if(Application.isPlaying)
        {
            GlobalTime = _globalTime;
        }
    }


    public virtual void SetPosition(PositionData[] data)
    {
        Positions = data;
        InitBezier();
    }

    private void ChangeGlobalTime()
    {
        PositionData prevPoint;
        PositionData nextPoint;
        int index;
        FindCurrentPositions(out prevPoint, out nextPoint, out index);
        if(nextPoint == null)
        {
            transform.position = prevPoint.Position;
        }
        else
        {
            float oneTimeSize = 1f / (Positions.Length - 1);
            float localLerpTime = (GlobalTime - prevPoint.Time) / (nextPoint.Time - prevPoint.Time);
            float currntTimeLine = Mathf.Lerp(index * oneTimeSize, (index + 1) * oneTimeSize, localLerpTime);
            transform.position = BezierSpline.GetPoint(currntTimeLine);
            transform.LookAt(transform.position + BezierSpline.GetDirection(currntTimeLine));
        }
    }

    private void FindCurrentPositions(out PositionData prevPoint, out PositionData nextPoint, out int index)
    {
        prevPoint = null;
        nextPoint = null;
        index = 0;
        for (int i = 0; i < Positions.Length; i++)
        {
            if(Positions[i].Time > GlobalTime)
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
            index = Positions.Length - 1;
            prevPoint = Positions[index];
        }
    }

    private bool BezierInitied;

    private void InitBezier()
    {
        if(BezierInitied)
        {
            return;
        }
        Debug.LogError("InitBezier");
        BezierInitied = true;
        BezierSpline.points = new Vector3[Positions.Length * 3 - 2];
        BezierSpline.modes = new CatlikeBezierControlPointMode[(int)Mathf.Ceil((BezierSpline.points.Length + 1)/3f)];

        for (int i = 0; i < Positions.Length; i++)
        {
            BezierSpline.SetControlPoint(i * 3, Positions[i].Position);
            Vector3 currenControllPointPos = Positions[i].Position;

            if(i != 0)
            {
                Vector3 preDirPos = BezierSpline.points[(i-1) * 3 + 1];
                BezierSpline.points[i * 3 - 1] = Vector3.Lerp(preDirPos, currenControllPointPos, 0.66f);
            }
            if(i != 0 && i != Positions.Length - 1)
            {
                Vector3 toNextCPDistance = Positions[i+1].Position - currenControllPointPos;
                Vector3 prev = (currenControllPointPos - BezierSpline.points[i * 3 - 1]).normalized;
                BezierSpline.points[i * 3 + 1] = currenControllPointPos +( prev * (toNextCPDistance.magnitude / 3));
            }
        }
    }
}
[System.Serializable]
public class PositionData
{
    public int Time;
    public Vector3 Position;
}
