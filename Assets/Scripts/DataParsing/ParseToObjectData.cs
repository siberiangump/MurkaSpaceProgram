using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseToObjectData : MonoBehaviour
{

	[SerializeField] ObjectData Target;
    [SerializeField] string FileName;

    [ContextMenu ("Parse")]
    public void Parse()
    {
    }

}
