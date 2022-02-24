using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    /*
     * Action delegate의 instance keyAction 생성
     * keyAction을 사용할 클래스들의 start 부분에서 
     * 각자 KeyAction에 추가할 함수들을 추가
     * 이렇게 되면 KeyAction을 사용하는 각 클래스들의 함수는 
     * KeyAction을 거쳐 사용하게 되고 다 기록이 남게됨.
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
