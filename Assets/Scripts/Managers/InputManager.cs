using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> mouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;
   
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && keyAction != null)
            keyAction.Invoke();

        if(mouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    //Mouse�� �� ���¿��� ó������ ����
                    mouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                mouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                    {                       
                        mouseAction.Invoke(Define.MouseEvent.Click);
                    }
                    mouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _pressed = false;
                _pressedTime = 0;   
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        mouseAction = null;
    }
}
