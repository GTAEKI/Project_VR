using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// 캐릭터 상태 enum
    /// 김민섭_231013
    /// </summary>
    public enum CharacterState
    {
        IDLE, MOVE, DIE
    }

    protected CharacterState state;     // 캐릭터의 현재 상태 , 김민섭_231013

    /// <summary>
    /// 캐릭터의 현재 상태 프로퍼티
    /// 김민섭_231013
    /// </summary>
    public virtual CharacterState State
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
                case CharacterState.DIE: break;
            }
        }
    }

    protected virtual void Update()
    {
        switch(State)
        {
            case CharacterState.IDLE: UpdateIdle(); break;
            case CharacterState.MOVE: UpdateMove(); break;
            case CharacterState.DIE: UpdateDie(); break;
        }
    }

    /// <summary>
    /// IDLE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected virtual void UpdateIdle() { }

    /// <summary>
    /// Move 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected virtual void UpdateMove() { }

    /// <summary>
    /// Die 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected virtual void UpdateDie() { }
}
