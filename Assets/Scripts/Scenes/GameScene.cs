using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Coroutine !
// 1. 함수의 상태를 저장/복원 가능!
    // -> 엄청 오래걸리는 작업을 잠시 끊거나
    // -> 원하는 타이밍에 함수를 잠시 멈추고/ 복원 하는 경우
// 2. return시 -> 우리가 원하는 타입으로 가능 (class도 가능)

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //  Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraCotroller>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "DogKnight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);

    }

    public override void Clear()
    {

    }
}
