using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private BulletStatus status;
    private Transform target;

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
        Debug.Log("데미지가 들어가고 있나?");

        if (other.tag == "WeaknessPoint" || other.tag == "Boss" || other.tag == "Minion")
        {
            Destroy(transform.gameObject);

            OnDamaged(other);
        }
    }
    public void OnDamaged(Collider target)
    {
        int damage = 0;

        // TODO : 
        if (target.tag == "WeaknessPoint")
        {
            Boss boss = target.GetComponentInParent<Boss>();

            damage = (int)(status.Damage * (1f + boss.CurrStatus.WeakpointRate));
            boss.CurrStatus.OnDamaged(damage);
        }
        else if(target.tag == "Boss")
        {
            Boss boss = target.GetComponent<Boss>();

            damage = (int)status.Damage;
            boss.CurrStatus.OnDamaged(damage);

        }
        else if( target.tag == "Minion")
        {
            MinionController minion = target.GetComponent<MinionController>();

            damage = (int)status.Damage;
            minion.CurrStatus.OnDamaged(damage);
        }
        else if( target.tag == "Meteor")
        {
            MeteorController meteor = target.GetComponent<MeteorController>();
            damage = (int)status.Damage;
            meteor.Status.OnDamaged(damage);
        }
        #region 데미지 출력
        Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
        TextMeshProUGUI textDamage;  // 데미지 출력 Text
        textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

        Vector3 currentPos = target.transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
        GameObject.Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
        canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

        textDamage.text = $"{damage}";  // Text에 데미지가 보여지게 한다.
        #endregion

    }
    public void Seek(Transform targetTransform)
    {
        target = targetTransform;
    }
}
