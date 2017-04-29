using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimelineDragListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsDragging { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
    }
}
