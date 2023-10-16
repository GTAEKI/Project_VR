using System.Collections;
using UnityEngine;

public interface ISearchTarget
{
    /// <summary>
    /// 타겟을 탐색하는 함수
    /// 김민섭_231015
    /// </summary>
    public void SearchTarget();

    /// <summary>
    /// 타겟을 향해 발사하는 함수
    /// 김민섭_231015
    /// </summary>
    /// <param name="targetPos">목표 위치</param>
    public void Fire(Vector3 targetPos);

    /// <summary>
    /// 목표 위치로 가속도 이동 함수
    /// 김민섭_231015
    /// </summary>
    public IEnumerator TargetingMove();
}
