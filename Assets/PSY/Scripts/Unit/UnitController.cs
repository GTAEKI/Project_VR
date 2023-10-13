using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 유닛 관리 클래스
public class UnitController : MonoBehaviour
{
    protected CurrentUnitStatus currentUnitStatus;     // 현재 스텟
    protected BaseUnitStatus baseUnitStatus;           // 원본 스텟

    protected UnitDataManager unitDataManager; // 유닛 데이터 csv


    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ( 유닛 )초기화 함수
    /// 231013_박시연
    /// </summary>
    protected virtual void Init()
    {
        unitDataManager = GameObject.Find("@Managers").GetComponent<UnitDataManager>();
    }

}
