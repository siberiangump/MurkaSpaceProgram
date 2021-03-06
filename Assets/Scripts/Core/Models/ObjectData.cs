using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ObjectData", menuName = "ScriptableObject/ObjectData", order = 1000000001)]
public class ObjectData : ScriptableObject
{
    public string Id;
    public SpaceObject Prefab;
    public List <TransferTrajectoryData> TrajectoryDataSet;
}