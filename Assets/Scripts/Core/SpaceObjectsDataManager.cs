using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectsDataManager : MonoBehaviour
{
    [SerializeField] ObjectData[] Objects;

    [SerializeField] List<SpaceObject> CurrentObjects; 

    public float ObjectScale = 0.5f;
    private GameObject rootGameObject;

    void Start()
    {
        Init();
    }

    [ContextMenu ("Init")]
    public void Init()
    {
        
        ClearCurrent();
        rootGameObject = new GameObject();
        rootGameObject.name = "root";
        rootGameObject.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
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
//        for (int i = 0; i < CurrentObjects.Count; i++)
//        {
//            CurrentObjects[i].transform.localScale *= ObjectScale;
//        }
        rootGameObject.transform.localScale = Vector3.one * ObjectScale;
    }

    private void InitObject(ObjectData data)
    {
        SpaceObject spaceObject = Instantiate(data.Prefab);
        spaceObject.Init(data);
        CurrentObjects.Add(spaceObject);
        spaceObject.transform.parent = rootGameObject.transform;
    }

    public void ClearCurrent()
    {
        for (int i = 0; i < CurrentObjects.Count; i++)
        {
            Destroy(CurrentObjects[i].gameObject);
        }

    }
}
