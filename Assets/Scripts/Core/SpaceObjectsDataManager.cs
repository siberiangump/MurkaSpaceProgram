using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectsDataManager : MonoBehaviour
{
    [SerializeField] ObjectData[] Objects;

    [SerializeField] List<SpaceObject> CurrentObjects; 

    public void Init()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            InitObject(Objects[i]);
        }
    }

    private void InitObject(ObjectData data)
    {
        SpaceObject spaceObject = Instantiate(data.Prefab);
        spaceObject.Init();
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
