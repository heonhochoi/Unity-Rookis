using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Coroutine !
// 1. �Լ��� ���¸� ����/���� ����!
    // -> ��û �����ɸ��� �۾��� ��� ���ų�
    // -> ���ϴ� Ÿ�ֿ̹� �Լ��� ��� ���߰�/ ���� �ϴ� ���
// 2. return�� -> �츮�� ���ϴ� Ÿ������ ���� (class�� ����)

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init(); 

        SceneType = Define.Scene.Game;

        //  Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();
    }

    public override void Clear()
    {

    }
}
