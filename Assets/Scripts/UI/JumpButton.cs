using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isDown;

    public Action<bool> buttonCb;

    public bool clicked;

    public void OnClickEvent()
    {
        // GameManager.instance.go_Player.GetComponent<PlayerControl>().DoJump();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonCb?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonCb?.Invoke(false);
        
    }

    public void SetCb(Action<bool> _buttonCb)
    {
        buttonCb = _buttonCb;
    }
}
