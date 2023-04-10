using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action<PointerEventData> OnClickHandler; // <PointerEventData> ���� �ʿ� ������������ // 
    public Action<PointerEventData> OnDragHandler; // ��� �ʿ��� �� //
    public Action<bool> OnPointerHandler;

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
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerHandler != null)
            OnPointerHandler.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerHandler != null)
            OnPointerHandler.Invoke(false);
    }
}
