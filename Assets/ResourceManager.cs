using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager// : MonoBehaviour
{
    public T Load<T>(string path) where T :Object
    {
        // �̰Ŵ� ����  �� �ﳪ ?

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
        // ���� prefab �� ����ؼ� Load �� ���ؼ� ȣ���� ��� ������ �߻����� �ʴ°� ?
        return Object.Instantiate(go, parent);
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
