using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총알 관리 클래스
public class BulletController : MonoBehaviour
{
    protected CurrentBulletStatus currentBulletStatus;     // 현재 스텟
    protected BaseBulletStatus baseBulletStatus;           // 원본 스텟

    protected BulletDataManager bulletDataManager; // 총알 데이터 csv

    public Vector3 dir; // 방향 변수

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ( 총알 )초기화 함수
    /// 231013_박시연
    /// </summary>
    protected virtual void Init()
    {
        bulletDataManager = GameObject.Find("@Managers").GetComponent<BulletDataManager>();

        StartCoroutine(DelayDestory(0));
    }

    private void Update()
    {
        transform.position += dir.normalized * Time.deltaTime;
    }

    /// <summary>
    /// LifeTime에 따른 총알 제거 코루틴
    /// </summary>
    /// <param name="index">총알 번호</param>
    /// <returns></returns>
    public IEnumerator DelayDestory(int index)
    {
        float lifeTime = float.Parse(bulletDataManager.bulletDatas[index]["LifeTime"].ToString());

        while (true)
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
