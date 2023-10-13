using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : Character
{
    /// <summary>
    /// 보스 상태 new enum
    /// 김민섭_231013
    /// </summary>
    public enum  CurrentPattern
    {
       METEOR, SUMMON, NONE
    }

    [Header("패턴 상태")]
    [SerializeField] private CurrentPattern currPattern = CurrentPattern.NONE;     // 보스의 현재 상태, 김민섭_231013

    private GameObject playerTarget;        // 공격 목표 캐릭터 , 김민섭_231013

    /// <summary>
    /// 보스 현재 상태 프로퍼티
    /// 김민섭_231013
    /// </summary>
    public CurrentPattern CurrPattern
    {
        get => currPattern;
        set
        {
            currPattern = value;

            // TODO: state마다 애니메이션 실행
            switch(currPattern)
            {
                case CurrentPattern.METEOR: break;
                case CurrentPattern.SUMMON: break;
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (State == CharacterState.SKILL)
        {   // 스킬 시전 상태일 경우 실행
            switch (CurrPattern)
            {
                case CurrentPattern.METEOR: UpdateMeteor(); break;
                case CurrentPattern.SUMMON: UpdateSummon(); break;
            }
        }
    }

    /// <summary>
    /// 보스가 IDLE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateIdle()
    {
        if(playerTarget != null)
        {   // 타겟이 있다면 움직임 시작
            State = CharacterState.MOVE;
            return;
        }

        if(playerTarget == null)
        {   // 타겟이 없다면 탐색 시작
            Search();
        }
    }

    /// <summary>
    /// 타겟을 탐색하는 함수
    /// 김민섭_231013
    /// </summary>
    private void Search()
    {
        Collider[] findTarget = Physics.OverlapSphere(transform.position, 100f, LayerMask.GetMask("Player"));
        if (findTarget.Length <= 0)
        {
            Debug.LogWarning("타겟{플레이어}를 찾을 수 없습니다.");
            return;
        }

        // 타겟을 찾았으면 움직임 상태로 전환
        playerTarget = findTarget.First().gameObject;
        startPosition = transform.position;
        endPosition = playerTarget.transform.position;
        endPosition.y = 2.5f;       // 임시 코드 , 김민섭_231013
        State = CharacterState.MOVE;
        return;
    }

    /// <summary>
    /// 보스가 MOVE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateMove()
    {
        if(playerTarget == null)
        {
            Debug.LogWarning("타겟{플레이어}를 찾을 수 없습니다.");
            State = CharacterState.IDLE;
            return;
        }

        // 타겟이 있다면 타겟을 향해 이동
        currentTime += Time.deltaTime * movementSpeed;

        if(currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        transform.position = Vector3.Lerp(startPosition, endPosition, currentTime / lerpTime);
    }

    /// <summary>
    /// 보스가 METEOR 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    private void UpdateMeteor()
    {
        Debug.Log("메테오 발동!");
    }

    /// <summary>
    /// 보스가 SUMMON 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    private void UpdateSummon()
    {
        Debug.Log("쫄몹 소환함");
    }

    /// <summary>
    /// 보스가 DIE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateDie()
    {
        Debug.Log("죽음");
    }
}
