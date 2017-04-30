using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeParrametrs : Singleton<SpaceTimeParrametrs>
{
    public static int Scale = 1000;

    public static Transform Center
    {
        get
        {
            return Instance.transform;
        }
    }

    protected override void OnSingletonAwake()
    {
        transform.parent = null;
        transform.position = Vector3.zero;
        base.OnSingletonAwake();
    }
}
