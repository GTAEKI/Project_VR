using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    /// <summary>
    /// 보스 상태 new enum
    /// 김민섭_231013
    /// </summary>
    public new enum  CharacterState
    {
       IDLE, MOVE, METEOR, SUMMON, DIE
    }

    protected new CharacterState state;     // 보스의 현재 상태, 김민섭_231013

    /// <summary>
    /// 보스 현재 상태 프로퍼티
    /// 김민섭_231013
    /// </summary>
    public new CharacterState State
    {
        get => state;
        set
        {
            state = value;

            // TODO: state마다 애니메이션 실행
            switch(state)
            {
                case CharacterState.IDLE: break;
                case CharacterState.MOVE: break;
                case CharacterState.METEOR: break;
                case CharacterState.SUMMON: break;
                case CharacterState.DIE: break;
            }
        }
    }

    protected override void Update()
    {
        switch(State)
        {
            case CharacterState.IDLE: UpdateIdle(); break;
            case CharacterState.MOVE: UpdateMove(); break;
            case CharacterState.METEOR: UpdateMeteor(); break;
            case CharacterState.SUMMON: UpdateSummon(); break;
            case CharacterState.DIE: UpdateDie(); break;
        }
    }

    /// <summary>
    /// 보스가 IDLE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateIdle()
    {

    }

    /// <summary>
    /// 보스가 MOVE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateMove()
    {

    }

    /// <summary>
    /// 보스가 METEOR 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    private void UpdateMeteor()
    {

    }

    /// <summary>
    /// 보스가 SUMMON 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    private void UpdateSummon()
    {

    }

    /// <summary>
    /// 보스가 DIE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateDie()
    {

    }
}
