using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    

    enum Buttons
    {
        PointButton,
    }

    enum Texts
    {
        PointText,
        ScoreText,

    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,

    }


    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonCllicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go, ((PointerEventData data) => { go.transform.position = data.position; }),Define.UIEvent.Click);
    }
    
    int _score = 0;

    public void OnButtonCllicked(PointerEventData data)
    {
        _score++;

        GetText((int)Texts.ScoreText).text = $" ���� : {_score}";
    }
}
