using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public const int STARTMONEY = 1000; // 초기 재화
    public const int NORMAL_MONSTER_MONEY = 10; // 일반 몬스터 처치시 재화
    public const int BOSS_MONSTER_MONEY = 100; // 보스 몬스터 공격시 기본 재화

    public int myMoney = default; // 내가 현재 갖고있는 재화

    // Managers에서 Init()실행
    public void Init()
    {
        myMoney = STARTMONEY; //게임 시작시 초기 재화 설정
    }

    /// <summary>
    /// 보스 공격시 획득하는 돈을 계산 및 추가 함수
    /// 배경택_231013
    /// </summary>
    /// <param name="damage"> 공격 데미지 입력 </param>
    public void BossHitMoney(int damage)
    {
        myMoney += BOSS_MONSTER_MONEY * damage; // 데미지에 비례하여 돈을 획득
    }

    /// <summary>
    /// 일반 몬스터 사망시 돈을 추가하는 함수
    /// </summary>
    public void NormalMonsterDie()
    {
        myMoney += NORMAL_MONSTER_MONEY; // 일반 몬스터 처치시 재화 상승
    }
}
