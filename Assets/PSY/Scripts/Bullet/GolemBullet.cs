using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 공격 유닛의 총알 클래스
public class GolemBullet : BulletController
{
    protected override void Init()
    {
        base.Init();

        currentBulletStatus = new BulletStatus(Define.Data_ID_List.Bullet_Golem);       // 골렘 특화 총알 데이터 초기화, 김민섭_231014

        rb.velocity = transform.forward * float.Parse(Managers.Data.ProjectileTable[(int)Define.Data_ID_List.Bullet_Golem]["Speed"].ToString());

        StartCoroutine(DelayDestory((int)Define.Data_ID_List.Bullet_Golem));        // 데이터에 맞춰서 파괴 설정, 김민섭_231014
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
