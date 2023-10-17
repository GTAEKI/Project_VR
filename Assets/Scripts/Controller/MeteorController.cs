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
        groundPos.y = 1.25f;

        // 기준점에서 레이를 쏨
        Ray ray = new Ray(groundPos, -transform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, LayerMask.GetMask("Player")))
        {   // 플레이어가 감지되었다면
            Util.DrawTouchRay(transform.position, hitInfo.point, Color.blue);
            Fire(hitInfo.point);
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

        yield return new WaitForSeconds(Random.Range(1f, 2f));

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
                Debug.Log("메테오 폭발!");
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
        if(other.tag == "Player")
        {
            KJHPlayer player = FindObjectOfType<KJHPlayer>();
            if(player != null)
            {
                player.status.OnDamaged(ref player.currHp, status.Damage);
            }

            Managers.Resource.Destroy(gameObject);
        }
    }
}
