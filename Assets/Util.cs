using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Util// : MonoBehaviour
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool rescursive = false) where T : Object
    {
        if (go == null)
            return null;

        if(rescursive == false) // 본인의 직계만 확인 ! 
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);

                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach(T components in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || components.name == name)
                    return components;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool resursive = false)// where T :Object
    {
        if (go == null)
            return null;

        Transform transform = FindChild<Transform>(go, name, resursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }
}
