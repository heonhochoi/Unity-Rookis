using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    /*
     * Action delegate�� instance keyAction ����
     * keyAction�� ����� Ŭ�������� start �κп��� 
     * ���� KeyAction�� �߰��� �Լ����� �߰�
     * �̷��� �Ǹ� KeyAction�� ����ϴ� �� Ŭ�������� �Լ��� 
     * KeyAction�� ���� ����ϰ� �ǰ� �� ����� ���Ե�.
     */
    public Action keyAction = null;
    public Action<Define.MouseEvent> mouseAction = null;

    bool _mousePressed = false;
   
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
                mouseAction.Invoke(Define.MouseEvent.Press);
                _mousePressed = true;
            }
            else
            {
                if(_mousePressed)
                    mouseAction.Invoke(Define.MouseEvent.Click);

                _mousePressed = false;
            }
        }
    }
}
