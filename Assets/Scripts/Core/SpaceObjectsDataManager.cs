using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectsDataManager : MonoBehaviour
{
    [SerializeField] ObjectData[] Objects;

    [SerializeField] List<SpaceObject> CurrentObjects; 

    void Start()
    {
        Init();
    }

    [ContextMenu ("Init")]
    public void Init()
    {
        ClearCurrent();
        for (int i = 0; i < Objects.Length; i++)
        {
            InitObject(Objects[i]);
            List<TransferTrajectoryData> trajectoryData =  Objects[i].TrajectoryDataSet;
            if(trajectoryData.Count>1)
            {
            TimeManager.Instance.SetStart(trajectoryData[0].UTCTime);
            TimeManager.Instance.SetEnd(trajectoryData[trajectoryData.Count-1].UTCTime);
            }
        }  
    }

    private void InitObject(ObjectData data)
    {
        SpaceObject spaceObject = Instantiate(data.Prefab);
        spaceObject.Init(data);
        CurrentObjects.Add(spaceObject);
    }

    public void ClearCurrent()
    {
        for (int i = 0; i < CurrentObjects.Count; i++)
        {
            Destroy(CurrentObjects[i].gameObject);
        }

    }
}
