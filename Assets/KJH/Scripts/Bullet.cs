using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletStatus status;
    private Transform target;
    public GameObject hit;
    private Rigidbody rb;

    void Start()
    {
        status = new BulletStatus(Define.Data_ID_List.Bullet_Normal);
        GetComponent<Rigidbody>().AddForce(transform.forward * status.Speed);
        rb = GetComponent<Rigidbody>();
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
        if (other.tag == "WeaknessPoint" || other.tag == "CenterPoint" || other.tag == "Minion" || other.tag == "Meteor")
        {
            Debug.Log(other);

            //{ 경택 _ 231020 _ hit이펙트 생성되는지 테스트
            if (hit != null)
            {
                var hitinstance = Instantiate(hit, transform.position, Quaternion.identity);
                var hitps = hitinstance.GetComponent<ParticleSystem>();
                if (hitps != null)
                {
                    Destroy(hitinstance, hitps.main.duration);
                }
                else
                {
                    var hitpsparts = hitinstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitinstance, hitpsparts.main.duration);
                }
            }
            // } 경택 _ 231020 _ hit이펙트 생성되는지 테스트

            OnDamaged(other);
            DestroyDelayed(0.5f);
            //Destroy(transform.gameObject);
        }
    }

    public void DestroyDelayed(float time)
    {
        rb.velocity = Vector3.zero;
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, time);
    }

    public void OnDamaged(Collider target)
    {
        int damage = 0;

        Debug.Log(target.tag + " " + target.name);

        if (target.tag != null)
        {
            // TODO : 
            if (target.tag == "WeaknessPoint")
            {
                Boss boss = target.transform.parent.parent.parent.GetComponent<Boss>();

                Aim aim = FindObjectOfType<Aim>();
                if(aim != null)
                {
                    if (aim.isPotion)
                    {
                        damage = (int)((status.Damage + 3) * (1f + boss.CurrStatus.WeakpointRate));
                    }
                    else
                    {
                        damage = (int)((status.Damage) * (1f + boss.CurrStatus.WeakpointRate));
                    }
                }

                boss.CurrStatus.OnDamaged(damage);
            }
            else if (target.tag == "CenterPoint")
            {
                Boss boss = target.transform.parent.GetComponent<Boss>();

                Aim aim = FindObjectOfType<Aim>();
                if (aim != null)
                {
                    if (aim.isPotion)
                    {
                        damage = (int)status.Damage + 3;
                    }
                    else
                    {
                        damage = (int)status.Damage;
                    }
                }
                boss.CurrStatus.OnDamaged(damage);

            }
            else if (target.tag == "Minion")
            {
                damage = (int)(status.Damage);

                PowerMinionController powerMinion = target.GetComponent<PowerMinionController>();
                if(powerMinion != null)
                {
                    powerMinion.CurrStatus.OnDamaged(damage);
                }
                else
                {
                    FastMinionController fastMinion = target.GetComponent<FastMinionController>();
                    if(fastMinion != null)
                    {
                        fastMinion.CurrStatus.OnDamaged(damage);
                    }
                }
            }
            else if (target.tag == "Meteor")
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
        else
        {
            return;
        }

    }
    public void Seek(Transform targetTransform)
    {
        target = targetTransform;
    }

    //public void CreateHitEffect()
    //{
    //    // 모든 축의 이동과 회전을 고정
    //    rb.constraints = rigidbodyconstraints.freezeall;
    //    speed = 0; // 이동 속도를 0으로 설정

    //    // 충돌 지점의 정보 가져오기
    //    contactpoint contact = collision.contacts[0];
    //    quaternion rot = quaternion.fromtorotation(vector3.up, contact.normal);
    //    vector3 pos = contact.point + contact.normal * hitoffset; // 충돌 지점 계산

    //    // 충돌 이펙트가 지정되어 있다면
    //    if (hit != null)
    //    {
    //        // 충돌 이펙트 생성 및 방향 설정
    //        var hitinstance = instantiate(hit, pos, rot);
    //        if (usefirepointrotation) { hitinstance.transform.rotation = gameobject.transform.rotation * quaternion.euler(0, 180f, 0); }
    //        else if (rotationoffset != vector3.zero) { hitinstance.transform.rotation = quaternion.euler(rotationoffset); }
    //        else { hitinstance.transform.lookat(contact.point + contact.normal); }

    //        var hitps = hitinstance.getcomponent<particlesystem>();

    //        // 파티클 시스템의 지속 시간만큼 후에 충돌 이펙트 제거
    //        if (hitps != null)
    //        {
    //            destroy(hitinstance, hitps.main.duration);
    //        }
    //        else
    //        {
    //            var hitpsparts = hitinstance.transform.getchild(0).getcomponent<particlesystem>();
    //            destroy(hitinstance, hitpsparts.main.duration);
    //        }
    //    }

    //    // 분리된 프리팹들의 부모 설정 해제
    //    foreach (var detachedprefab in detached)
    //    {
    //        if (detachedprefab != null)
    //        {
    //            detachedprefab.transform.parent = null;
    //        }
    //    }
    //}
}
