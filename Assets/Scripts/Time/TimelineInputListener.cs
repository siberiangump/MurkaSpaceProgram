using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimelineInputListener : MonoBehaviour, ISelectHandler, ISubmitHandler, IDeselectHandler
{ 
    public bool IsEditing { get; set; }

    public void OnSelect(BaseEventData eventData)
    {
        IsEditing = true;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        IsEditing = false;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        IsEditing = false;
    }
}
