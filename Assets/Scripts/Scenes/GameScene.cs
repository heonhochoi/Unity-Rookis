using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // for문에서 사용할 때
            for(int i=0;i<100000;i++)
            {
                if (i % 10000 == 0)
                    yield return null;
            }
            // 이렇게 하면 10000번을 기준으로 함수의 제어권을 
            // 다른 지역함수로 넘길 수 있다. 

            yield return new Test() { id = 1 };
   
            // Skip하고 싶을 때
            yield return null;

            yield return new Test() { id = 2 };

            // 영구 종료 하고 싶을 때 
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
