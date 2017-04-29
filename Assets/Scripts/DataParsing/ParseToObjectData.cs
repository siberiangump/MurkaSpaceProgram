using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ParseToObjectData : MonoBehaviour
{

    [SerializeField]
    ObjectData Target;
    [SerializeField]
    string FileName;

    [ContextMenu("Parse")]
    public void Parse()
    {
        StreamReader reader = File.OpenText(FileName);
        string line;
        Target.TrajectoryDataSet.Clear();
        reader.ReadLine();
        while ((line = reader.ReadLine()) != null)
        {
            TransferTrajectoryData tmp = new TransferTrajectoryData();
            string[] items = line.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
            DateTime date;
            DateTime.TryParse(items[0], out date);
            tmp.UTCTime = date.ToFileTimeUtc();
            float.TryParse(items[1], out tmp.Position.x);
            float.TryParse(items[2], out tmp.Position.y);
            float.TryParse(items[3], out tmp.Position.z);

            Target.TrajectoryDataSet.Add(tmp);
        }
        UnityEditor.EditorUtility.SetDirty(Target);
    }
}
