using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 졸개 공격 유닛의 총알
public class MinonBullet : BulletController
{
    protected override void Init()
    {
        base.Init();

        baseBulletStatus = new BaseBulletStatus(
            int.Parse(bulletDataManager.bulletDatas[0]["ID"].ToString()),
            bulletDataManager.bulletDatas[0]["Info"].ToString(),
            float.Parse(bulletDataManager.bulletDatas[0]["Delay"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[0]["Damage"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[0]["Speed"].ToString()),
            float.Parse(bulletDataManager.bulletDatas[0]["LifeTime"].ToString())
            );

        currentBulletStatus = new CurrentBulletStatus(baseBulletStatus);

        rb.velocity = transform.forward * float.Parse(bulletDataManager.bulletDatas[0]["Speed"].ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Minion" || other.tag == "Boss")
        {
            //currentBulletStatus.OnDamaged(other);
            Destroy(transform.gameObject);
        }
    }
}
