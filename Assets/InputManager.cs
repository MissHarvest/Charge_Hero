using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager// : MonoBehaviour
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }
}
