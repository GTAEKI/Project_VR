using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f; // 발사체 이동 속도
    public float hitOffset = 0f; // 충돌 지점에서의 오프셋
    public bool UseFirePointRotation; // 발사 시점의 회전값 사용 여부
    public Vector3 rotationOffset = new Vector3(0, 0, 0); // 회전 오프셋 값
    public GameObject hit; // 충돌 시 생성할 이펙트 오브젝트
    public GameObject flash; // 발사 시 생성할 플래시 이펙트 오브젝트
    private Rigidbody rb; // Rigidbody 컴포넌트
    public GameObject[] Detached; // 발사체에서 분리될 프리팹 오브젝트 배열

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기

        //// 플래시 이펙트가 지정되어 있다면
        //if (flash != null)
        //{
        //    var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
        //    flashInstance.transform.forward = gameObject.transform.forward;
        //    var flashPs = flashInstance.GetComponent<ParticleSystem>();

        //    // 파티클 시스템의 지속 시간만큼 후에 플래시 이펙트 제거
        //    if (flashPs != null)
        //    {
        //        Destroy(flashInstance, flashPs.main.duration);
        //    }
        //    else
        //    {
        //        var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
        //        Destroy(flashInstance, flashPsParts.main.duration);
        //    }
        //}

        // 5초 후에 발사체 제거
        //Destroy(gameObject, 10);
    }


    // 충돌 시 호출되는 함수
    void OnCollisionEnter(Collision collision)
    {
        //// 모든 축의 이동과 회전을 고정
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        //speed = 0; // 이동 속도를 0으로 설정

        //// 충돌 지점의 정보 가져오기
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point + contact.normal * hitOffset; // 충돌 지점 계산

        //// 충돌 이펙트가 지정되어 있다면
        //if (hit != null)
        //{
        //    // 충돌 이펙트 생성 및 방향 설정
        //    var hitInstance = Instantiate(hit, pos, rot);
        //    if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
        //    else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
        //    else { hitInstance.transform.LookAt(contact.point + contact.normal); }

        //    var hitPs = hitInstance.GetComponent<ParticleSystem>();

        //    // 파티클 시스템의 지속 시간만큼 후에 충돌 이펙트 제거
        //    if (hitPs != null)
        //    {
        //        Destroy(hitInstance, hitPs.main.duration);
        //    }
        //    else
        //    {
        //        var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
        //        Destroy(hitInstance, hitPsParts.main.duration);
        //    }
        //}

        //// 분리된 프리팹들의 부모 설정 해제
        //foreach (var detachedPrefab in Detached)
        //{
        //    if (detachedPrefab != null)
        //    {
        //        detachedPrefab.transform.parent = null;
        //    }
        //}

        //// 발사체 제거
        //Destroy(gameObject);
    }
}