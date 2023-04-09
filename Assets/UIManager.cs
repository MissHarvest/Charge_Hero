using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    int _order = 10;

    Stack<UI_Popup> _popupstack = new Stack<UI_Popup>();

    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if(root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; // 캔버스 중첩의 경우 부모 캔버스와 별개로 sort 값을 가지기 위함 //
        
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if(_sceneUI) Managers.Resource.Destroy(_sceneUI.gameObject);

        if(string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);// parent = Root.transform;

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T: UI_Popup
    {
        if(string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupstack.Push(popup);

        go.transform.SetParent(Root.transform);// .parent = Root.transform;

        return popup;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupstack.Count == 0)
            return;

        if (_popupstack.Peek() != popup)
            return;

        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (_popupstack.Count == 0)
            return;

        UI_Popup popup = _popupstack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null; // popup 이 가리키는 메모리를 null 하는 건가 ? popup 변수 자체는 어차피 사라지는데 
        _order--;
    }

    public T MakeSubUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if(string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/SubUI/{name}");

        if(parent != null)
        {
            go.transform.SetParent(parent);
        }

        return Util.GetOrAddComponent<T>(go);
    }
}
