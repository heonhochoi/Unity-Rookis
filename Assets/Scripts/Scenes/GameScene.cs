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
    class Test
    {
        public int id = 0;
    }

    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            // for������ ����� ��
            for(int i=0;i<100000;i++)
            {
                if (i % 10000 == 0)
                    yield return null;
            }
            // �̷��� �ϸ� 10000���� �������� �Լ��� ������� 
            // �ٸ� �����Լ��� �ѱ� �� �ִ�. 

            yield return new Test() { id = 1 };
   
            // Skip�ϰ� ���� ��
            yield return null;

            // ���� ���� �ϰ� ���� �� 
            yield break;
        }

    
    }

    protected override void Init()
    {
        base.Init(); 

        SceneType = Define.Scene.Game;
        
        Managers.UI.ShowSceneUI<UI_Inven>();

        CoroutineTest test = new CoroutineTest();
        foreach(System.Object t in test)
        {
            Test _test = (Test)t;
            int id = _test.id;
            Debug.Log(id);
        }
    }

    public override void Clear()
    {

    }
}
