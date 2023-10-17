using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private BulletStatus status;

    void Start()
    {
        status = new BulletStatus(Define.Data_ID_List.Bullet_Normal);
        GetComponent<Rigidbody>().AddForce(transform.forward * status.Speed);

        Managers.Resource.Destroy(gameObject, status.LifeTime);
    }

    void Update()
    {
    }



    /// <summary>
    /// 공격 데미지 주는 함수
    /// </summary>
    /// <param name="other">적</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WeaknessPoint" || other.tag == "Boss")
        {
            Destroy(transform.gameObject);

            OnDamaged(other);
        }
    }
    public void OnDamaged(Collider target)
    {
        // TODO : 
        if (target.tag == "WeaknessPoint")
        {
            Boss boss = target.GetComponentInParent<Boss>();

            boss.CurrStatus.OnDamaged((int)(status.Damage * (1f + boss.CurrStatus.WeakpointRate)));
            UnityEngine.Debug.Log($"약점 최종 데미지 : {(int)(status.Damage * (1f + boss.CurrStatus.WeakpointRate))}");

            #region 데미지 출력
            Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
            TextMeshProUGUI textDamage;  // 데미지 출력 Text
            textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 currentPos = target.transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
            GameObject.Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
            canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

            textDamage.text = $"{(int)(status.Damage * (1f + boss.CurrStatus.WeakpointRate))}";  // Text에 데미지가 보여지게 한다.
            #endregion
        }
    }
}
