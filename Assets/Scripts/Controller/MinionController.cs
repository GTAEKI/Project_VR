using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour, ISearchTarget
{
    [Header("TEST: 졸개 스탯")]
    [SerializeField] protected MinionStatus currStatus;       // 졸개 스텟, 김민섭_231015
    private Vector3 targetPosition;      // 날라가려는 위치, 김민섭_231015

    /// <summary>
    /// 졸개 현재 스탯 Get 프로퍼티
    /// 김민섭_231016
    /// </summary>
    public MinionStatus CurrStatus => currStatus;

    private void Start()
    {
        Init();
        SearchTarget();
    }

    /// <summary>
    /// 초기화 함수
    /// 김민섭_231014
    /// </summary>
    protected virtual void Init() { }

    private void Update()
    {
        transform.Rotate(new Vector3(-Time.deltaTime * (currStatus.Speed + 300f), 0f, 0f));
    }

    #region ISearchTarget 함수

    /// <summary>
    /// 플레이어를 탐색하는 함수
    /// 김민섭_231015
    /// </summary>
    public void SearchTarget()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, LayerMask.GetMask("Player")))
        {
            Util.DrawTouchRay(transform.position, hitInfo.point, Color.red);
            Fire(hitInfo.point);
        }
    }

    /// <summary>
    /// 공격 위치로 발사 함수
    /// 김민섭_231013
    /// </summary>
    /// <param name="targetPosition">목표 위치</param>
    public void Fire(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

        StartCoroutine(TargetingMove());
    }

    /// <summary>
    /// 목표 위치로 가속도 이동 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    public IEnumerator TargetingMove()
    {
        float currentTime = 0f;
        float lerpTime = 30f;
        Vector3 startPos = transform.position;

        yield return new WaitForSeconds(Random.Range(1f, 2f));

        while (true)
        {
            if (currStatus.IsDie)
            {
                Managers.MONEY.NormalMonsterDie();
                Managers.MONEY.ReflectMoney();
                Managers.Resource.Destroy(gameObject);
                yield break;
            }

            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance <= currStatus.Range_Att)
            {   // 공격 사거리에 들어오면 폭발
                KJHPlayer player = FindObjectOfType<KJHPlayer>();
                player?.status.OnDamaged(ref player.currHp, currStatus.Damage);

                // 졸개 폭발 이펙트 실행, 김민섭_231018
                GameObject explosion = Managers.Resource.Instantiate("Particle/MinionExplosion", transform.position, Quaternion.identity);
                Managers.Resource.Destroy(explosion, 3f);

                Managers.Sound.Play("SFX/SE_Minion_Explosion");

                Managers.Resource.Destroy(gameObject);
                yield break;
            }

            // 타겟이 있다면 타겟을 향해 이동
            currentTime += Time.deltaTime * currStatus.Speed;

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
}
