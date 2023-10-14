using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 졸개 공격 유닛의 총알
public class MinonBullet : BulletController
{
    protected override void Init()
    {
        base.Init();

        currentBulletStatus = new BulletStatus(Define.Data_ID_List.Bullet_Minion);      // 졸개 특화 총알 데이터 초기화, 김민섭_231014

        rb.velocity = transform.forward * float.Parse(Managers.Data.ProjectileTable[(int)Define.Data_ID_List.Bullet_Minion]["Speed"].ToString());
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
