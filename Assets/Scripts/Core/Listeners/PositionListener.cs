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
//            InitBezier();

            ChangeGlobalTime();
        }
    }

    [SerializeField]
    [Range(0.0f, 3.0f)]
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
//        InitBezier();
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
            float toDestinationLocalTime = nextPoint.Time - prevPoint.Time;
            float currentLocalTime = GlobalTime - prevPoint.Time;
            float currntTimeLine = currentLocalTime / toDestinationLocalTime;
            transform.position = Vector3.Lerp(prevPoint.Position, nextPoint.Position, currntTimeLine);
//            Debug.LogError(currntTimeLine);
//            currntTimeLine = currntTimeLine / Positions.Length;
//            transform.position = BezierSpline.GetPoint(currntTimeLine);
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
[System.Serializable]
public class PositionData
{
    public int Time;
    public Vector3 Position;
}
