using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager// : MonoBehaviour
{
    public T Load<T>(string path) where T :Object
    {
        // 이거는 오류  안 찍나 ?

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        //if (path.Contains("Prefabs/") == false) path = $"Prefabs/{path}";

        GameObject go = Load<GameObject>($"Prefabs/{path}");
        
        if(go == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        // 같은 prefab 을 계속해서 Load 를 통해서 호출할 경우 문제가 발생하진 않는가 ?
        return Object.Instantiate(go, parent);
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
