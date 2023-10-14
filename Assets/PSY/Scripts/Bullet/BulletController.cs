using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총알 관리 클래스
public class BulletController : MonoBehaviour
{
    protected BulletStatus currentBulletStatus;         // 현재 스텟, 김민섭_231014
    protected Rigidbody rb;  // 총알의 rigidbody

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
        rb = GetComponent<Rigidbody>();

        transform.LookAt(dir);
    }

    /// <summary>
    /// LifeTime에 따른 총알 제거 코루틴
    /// </summary>
    /// <param name="index">총알 번호</param>
    /// <returns></returns>
    public IEnumerator DelayDestory(int index)
    {
        float lifeTime = float.Parse(Managers.Data.ProjectileTable[index]["LifeTime"].ToString());

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
