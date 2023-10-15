using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour, ISearchTarget
{
    private Vector3 targetPosition;      // 날라가려는 위치, 김민섭_231015

    public void Start()
    {
        Init();
        SearchTarget();
    }

    private void Init()
    {
        Managers.Resource.Destroy(gameObject, 30f);
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
    /// 메테오 목표 위치로 발사 함수
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
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance <= 0.1f)
            {
                Debug.Log("졸개 폭발!");
                yield break;
            }

            // 타겟이 있다면 타겟을 향해 이동
            currentTime += Time.deltaTime * 5f;

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
        if (other.tag == "Player")
        {
            Debug.Log($"{transform.tag} -> {other.tag} 맞춤");
            Managers.Resource.Destroy(gameObject);
        }
    }
}
