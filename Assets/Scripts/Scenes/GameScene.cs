using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // 매니저 초기화
        Managers.Init();
        
        // 보스 소환
        Managers.Resource.Instantiate("Boss_Golem", new Vector3(0, 0, 150), Quaternion.Euler(new Vector3(0, 180, 0)));
        Managers.GameManager.SearchBoss();
    }

    public override void Clear()
    {

    }
}
