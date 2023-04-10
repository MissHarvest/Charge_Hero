using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponet<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
    public static void EventBind(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        // Debug.Log("Extension _ Bind");
        UI_Base.BindEvent(go, action, type);
    }
    public static void EventBind(this GameObject go, Action<bool> action, Define.UIEvent type = Define.UIEvent.Press)
    {
        UI_Base.BindEvent(go, action, type);
    }
}
