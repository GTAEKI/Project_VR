using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 공격 유닛의 클래스
public class Golem : UnitController
{
    #region 총알 관련 변수
    private BulletDataManager bulletDataManager;  // 총알 데이터 매니저
    private GameObject golemBullet;   // 골렘 공격 총알
    private Transform spawnPoint;     // 총알 생성 위치
    private List<GameObject> bullets = new List<GameObject>(); // 생성한 총알을 관리할 List
    #endregion

    #region 탐지 관련 변수
    private float radius = 1000f;     // 탐지할 범위
    private Collider targetCollider;  // 공격할 타겟의 콜라이더
    public Vector3 dir;               // 바라볼 타겟의 방향
    #endregion

    protected override void Init()
    {
        currentUnitStatus = new UnitStatus(Define.Data_ID_List.Unit_Golem);     // 골렘 특화 유닛 데이터 초기화, 김민섭_231014

        bulletDataManager = GameObject.Find("@Managers").GetComponent<BulletDataManager>();

        spawnPoint = transform.Find("BulletSpawnPoint");

        StartBulletAttack();
    }

    /// <summary>
    /// 총알 발사 함수
    /// 231013_박시연
    /// </summary>
    public void StartBulletAttack()
    {
        golemBullet = Resources.Load<GameObject>("Prefabs/GolemBullet");
        Debug.Log("Golem 총알 발사 ( Index : 1 )");

        StartCoroutine(SpawnBullet(golemBullet));
    }

    #region 코루틴 함수
    /// <summary>
    /// 총알 생성 코루틴 함수
    /// /// 231013_박시연
    /// </summary>
    /// <param name="createObj">생성할 총알</param>
    public IEnumerator SpawnBullet(GameObject createObj)
    {
        float Maxdelay = float.Parse(bulletDataManager.bulletDatas[1]["Delay"].ToString());  // 최대 딜레이 시간

        float delay = 0f;  // 현재 딜레이 시간

        while (true)
        {
            delay += Time.deltaTime;

            if (Maxdelay <= delay)  // 최대 딜레이 시간에 도달한다면
            {
                GameObject instance = Instantiate(createObj, spawnPoint.position, Quaternion.identity);
                bullets.Add(instance);  // 총알을 생성 장소에 맞게 생성한다.

                instance.GetComponent<BulletController>().dir = dir;  // 타겟의 방향을 설정해준다. 

                StartCoroutine(DestoryBullet(instance));
                delay = 0f;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 총알 삭제 코루틴 함수
    /// /// 231013_박시연
    /// </summary>
    /// <param name="instance">생성한 총알</param>
    public IEnumerator DestoryBullet(GameObject instance)
    {
        float lifeTime = float.Parse(bulletDataManager.bulletDatas[1]["LifeTime"].ToString());  // 총알의 유지 시간

        while (true)
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)  // 유지 시간이 0초에 도달했다면
            {
                Destroy(instance);  // 총알을 사라지게 한다.
                yield break;
            }

            yield return null;
        }
    }
    #endregion
}
