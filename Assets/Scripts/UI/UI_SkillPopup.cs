using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillPopup : UI_Popup
{
    enum Images
    {
        Skill_Img,
    }
    protected override void Init()
    {
        Bind<Image>(typeof(Images));
    }
    //public GameObject go_Scene;
    public float m_fSpeed;
    public int direction;
    public float m_fTime;
    public float m_fStopTime;
    RectTransform rectTransform;
    followCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        rectTransform = GetImage((int)Images.Skill_Img).gameObject.GetComponent<RectTransform>();
        rectTransform.transform.localPosition = new Vector3(-Screen.width, 0, 0);
    }
    public void Set(followCamera cam)
    {
        this.cam = cam;
    }
    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case 0:
                rectTransform.transform.localPosition += Vector3.right * m_fSpeed * Time.deltaTime;
                if (rectTransform.transform.localPosition.x >= 0)
                {
                    rectTransform.transform.localPosition = Vector3.zero;
                    direction = 1;
                }
                break;
            case 1:
                m_fTime += Time.deltaTime;
                if (m_fTime >= m_fStopTime) direction = 2;
                break;
            case 2:
                rectTransform.transform.localPosition += Vector3.right * m_fSpeed * Time.deltaTime;
                if (rectTransform.transform.localPosition.x >= Screen.width)
                {
                    cam.ChangeCamType(followCamera.E_type.closedown);
                    Managers.UI.ClosePopupUI(this);
                }
                break;
        }
    }
}
