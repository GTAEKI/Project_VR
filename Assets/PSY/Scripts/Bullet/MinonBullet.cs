using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// 졸개 공격 유닛의 총알
public class MinonBullet : BulletController
{
    protected override void Init()
    {
        base.Init();

        currentBulletStatus = new BulletStatus(Define.Data_ID_List.Bullet_Minion);      // 졸개 특화 총알 데이터 초기화, 김민섭_231014


        rb.velocity = transform.forward * float.Parse(Managers.Data.ProjectileTable[(int)Define.Data_ID_List.Bullet_Minion]["Speed"].ToString());

        StartCoroutine(DelayDestory((int)Define.Data_ID_List.Bullet_Minion));            // 데이터에 맞춰서 파괴 설정, 김민섭_231014
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Minion" || other.tag == "Boss")
        {
            currentBulletStatus.OnDamaged(other);

            #region 데미지 출력
            Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
            TextMeshProUGUI textDamage;  // 데미지 출력 Text
            textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 currentPos = transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
            Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
            canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

            textDamage.text = $"{currentBulletStatus.Damage}";  // Text에 데미지가 보여지게 한다.
            #endregion

            Destroy(transform.gameObject);
        }
    }
}
