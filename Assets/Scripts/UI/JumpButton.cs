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
        GameManager.instance.go_Player.GetComponent<PlayerControl>().DoJump();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        
        // GameManager.instance.go_Player.GetComponent<PlayerControl>().isJumpBtnDown = true;
        // 일단 isJumpBtnDown 이 private 이다.
        // 버튼이 눌릴 때 마다 실행되야 하는 함수가 늘어나면 
        // 여기 부분이 늘어난다.

        buttonCb?.Invoke(isDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;

        buttonCb?.Invoke(isDown);
    }

    public void SetCb(Action<bool> _buttonCb)
    {
        buttonCb = _buttonCb;
    }
}
