using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 총알 클래스
public class GolemBullet : BulletController
{
    protected override void Init()
    {
        base.Init();

        baseBulletStatus = new BaseBulletStatus(
            int.Parse(bulletDataManager.bulletDatas[1]["ID"].ToString()),
            bulletDataManager.bulletDatas[1]["Info"].ToString(),
            float.Parse(bulletDataManager.bulletDatas[1]["Delay"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[1]["Damage"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[1]["CriChance"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[1]["CriDamage"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[1]["Speed"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[1]["LifeTime"].ToString())
            );

        currentBulletStatus = new CurrentBulletStatus(baseBulletStatus);

        rb.velocity = transform.forward * float.Parse(bulletDataManager.bulletDatas[1]["Speed"].ToString());
    }
}
