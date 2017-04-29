using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] PositionListener Position;

    public void Init(ObjectData data)
    {
        this.transform.position = data.TrajectoryDataSet[0].Position;
        if(Position)
        {
            Position.SetPosition(data.TrajectoryDataSet);
            TimeManager.Instance.SubscribeOnTick(Position.OnTimeTick);
        }
    }
}
