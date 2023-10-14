using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 총알 클래스
public class GolemBullet : BulletController
{
    protected override void Init()
    {
        base.Init();

        currentBulletStatus = new BulletStatus(Define.Data_ID_List.Bullet_Golem);       // 골렘 특화 총알 데이터 초기화, 김민섭_231014

        rb.velocity = transform.forward * float.Parse(Managers.Data.ProjectileTable[(int)Define.Data_ID_List.Bullet_Golem]["Speed"].ToString());
    }
}
