using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 유닛 관리 클래스
public class UnitController : MonoBehaviour
{
    protected UnitStatus currentUnitStatus;            // 현재 스탯, 김민섭_231014

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ( 유닛 )초기화 함수
    /// 231013_박시연
    /// </summary>
    protected virtual void Init() { }
}
