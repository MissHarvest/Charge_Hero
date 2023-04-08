using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler;
    public Action<PointerEventData> OnDragHandler;

    public void OnPointerClick(PointerEventData eventdata)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventdata);
    }

    public void OnDrag(PointerEventData eventdata)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventdata);
    }
}
