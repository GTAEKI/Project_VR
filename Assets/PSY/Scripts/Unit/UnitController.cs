using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 유닛 관리 클래스
public class UnitController : MonoBehaviour
{
    [Header("TEST: 유닛 스탯")]
    [SerializeField] protected UnitStatus currentUnitStatus;            // 현재 스탯, 김민섭_231014

    /// <summary>
    /// 유닛 상태 enum
    /// 231014_박시연
    /// </summary>
    public enum State
    {
        Idle, Attack, Die
    }
    [SerializeField]
    protected State currentState = State.Idle;  // 유닛의 현재 상태

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
