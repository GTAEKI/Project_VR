using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour, ISearchTarget
{
    [Header("TEST: 메테오 스탯")]
    [SerializeField] private MeteorStatus status;         // 메테오 스탯, 김민섭_231014
    private Vector3 targetPosition;                       // 날라가려는 위치

    /// <summary>
    /// 메테오 스탯 Get 프로퍼티
    /// 김민섭_231014
    /// </summary>
    public MeteorStatus Status => status;

    private void Start()
    {
        Init();
        SearchTarget();
    }

    /// <summary>
    /// 초기화 함수
    /// 김민섭_231014
    /// </summary>
    private void Init()
    {
        status = new MeteorStatus(Define.Data_ID_List.Meteor);          // 스탯 세팅
        Managers.Resource.Destroy(gameObject, status.DurationTime);     // 파괴 예약
    }

    #region ISearchTarget 함수

    /// <summary>
    /// 플레이어를 탐색하는 인터페이스 함수
    /// 김민섭_231013
    /// </summary>
    public void SearchTarget()
    {
        // 지면을 기준점으로 정함
        Vector3 groundPos = transform.position;
        groundPos.y = 5f;

        // 기준점에서 레이를 쏨
        Ray ray = new Ray(groundPos, -transform.forward);
        RaycastHit[] hitInfos = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("Player"));
        RaycastHit hitTarget = default;

        for (int i = 0; i < hitInfos.Length; i++)
        {
            if (hitInfos[i].transform.tag == "Player")
            {
                hitTarget = hitInfos[i];
                break;
            }
        }

        if(hitTarget.collider != null)
        {
            Util.DrawTouchRay(transform.position, hitTarget.point, Color.blue);
            Fire(hitTarget.point);
        }
    }

    /// <summary>
    /// 메테오 목표 위치로 발사 인터페이스 함수
    /// 김민섭_231013
    /// </summary>
    /// <param name="targetPosition">목표 위치</param>
    public void Fire(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        StartCoroutine(TargetingMove());
    }

    /// <summary>
    /// 목표 위치로 가속도 이동 인터페이스 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    public IEnumerator TargetingMove()
    {
        float currentTime = 0f;
        float lerpTime = 30f;
        Vector3 startPos = transform.position;

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        while(true)
        {
            if(Status.IsDie)
            {
                Managers.Resource.Destroy(gameObject);
                yield break;
            }

            float distance = Vector3.Distance(transform.position, targetPosition);
            if(distance <= 0.1f)
            {
                yield break;
            }

            // 타겟이 있다면 타겟을 향해 이동
            currentTime += Time.deltaTime * status.Speed;

            if (currentTime >= lerpTime)
            {
                currentTime = lerpTime;
            }

            float t = currentTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // 움직임 함수
            transform.position = Vector3.Lerp(startPos, targetPosition, t);

            yield return null;
        }
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            KJHPlayer player = FindObjectOfType<KJHPlayer>();
            if (player != null)
            {
                player.status.OnDamaged(ref player.currHp, status.Damage);
            }

            // 메테오 폭발 이펙트 실행, 김민섭_231018
            GameObject explosion = Managers.Resource.Instantiate("Particle/MeteorExplosion", transform.position, Quaternion.identity);
            Managers.Resource.Destroy(explosion, 3f);

            Managers.Sound.Play("SFX/SE_Projectile_Boss_Destroy");

            Managers.Resource.Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        KJHPlayer player = FindObjectOfType<KJHPlayer>();
    //        if (player != null)
    //        {
    //            player.status.OnDamaged(ref player.currHp, status.Damage);
    //        }

    //        // 메테오 폭발 이펙트 실행, 김민섭_231018
    //        Managers.Resource.Instantiate("Particle/MeteorExplosion", transform.position, Quaternion.identity);

    //        Managers.Resource.Destroy(gameObject);
    //    }
    //}
}
