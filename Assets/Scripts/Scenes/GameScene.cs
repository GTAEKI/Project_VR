using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // 데이터 초기화
        Managers.Data.Init();
        Managers.Sound.Init();

        // 보스 소환
        Managers.Resource.Instantiate("Boss_Golem", new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(0, 180, 0)));
    }

    public override void Clear()
    {

    }
}
