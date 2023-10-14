using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 공격 유닛의 총알 클래스
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
            float.Parse(bulletDataManager.bulletDatas[1]["Speed"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[1]["LifeTime"].ToString())
            );

        currentBulletStatus = new CurrentBulletStatus(baseBulletStatus);

        rb.velocity = transform.forward * float.Parse(bulletDataManager.bulletDatas[1]["Speed"].ToString());
    }

    /// <summary>
    /// 공격 데미지 주는 함수
    /// </summary>
    /// <param name="other">적</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WeaknessPoint" || other.tag=="Boss")
        {
            currentBulletStatus.OnDamaged(other);
            Destroy(transform.gameObject);
        }
    }
}
