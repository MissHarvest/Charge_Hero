using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    protected abstract void Init();
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] name = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i< name.Length; i++)
        {
            if(typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, name[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, name[i], true);
            }
            if (objects[i] == null)
            {
                Debug.Log($"Failed to Bind({name[i]})");
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        //Debug.Log($"Get {typeof(T)}");
        UnityEngine.Object[] objects = null;
        // _objects.TryGetValue(typeof(T), out sdf); 배열 변수에 out 하고
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }
        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }

    public  static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        //Debug.Log($"{go.name}");
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }

    }

}
