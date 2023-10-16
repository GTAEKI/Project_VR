using System.Diagnostics;
using UnityEngine;

/// <summary>
/// 모든 기본 스탯 클래스
/// 김민섭_231014
/// </summary>
public class Status
{
    public int ID { protected set; get; }         // 고유 번호
}

#region 보스 관련 스테이터스

/// <summary>
/// 메테오 스탯 클래스
/// 김민섭_231014
/// </summary>
public class MeteorStatus: Status
{
    public int Hp { private set; get; }                 // 투사체 체력
    public float DurationTime { private set; get; }     // 투사체 활성화 시간
    public int Damage { private set; get; }             // 투사체 피해량
    public float Speed { private set; get; }            // 투사체 이동속도

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231014
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public MeteorStatus(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["ID"].ToString());
        Hp = int.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Hp"].ToString());
        DurationTime = float.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Projectile_Lifetime"].ToString());
        Damage = int.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Damage"].ToString());
        Speed = float.Parse(Managers.Data.Boss_MeteorStatusData[(int)id]["Speed"].ToString());
    }

    /// <summary>
    /// 데미지 부여 함수
    /// 김민섭_231014
    /// </summary>
    /// <param name="attacker">자신을 공격한 대상</param>
    public void OnDamaged(GameObject owner, GameObject attacker)
    {
        // TODO: attacker의 스탯 클래스 접근
        // TODO: 데미지 공식에 맞춰서 체력 감소 시킴

        if(Hp <= 0)
        {   // 체력이 0 이하이면 사망
            Hp = 0;
            Managers.Resource.Destroy(owner);
        }
    }
}

/// <summary>
/// 골렘 스탯 클래스
/// 김민섭_231014
/// </summary>
public class GolemStatus : Status
{
    // 테이블 데이터
    public float WeakpointRate { private set; get; }    // 약점 피격 시 받는 데미지 증가율
    public float ActTime { private set; get; }          // 비활성화된 약점이 다시 활성화될 때까지 소요되는 시간
    public int Hp { private set; get; }                 // 골렘 체력
    public float MoveSpeed { private set; get; }        // 골렘 이동속도
    public int Phase { private set; get; }              // 페이지
    
    // 일반 데이터
    public bool IsDie { private set; get; }             // 사망 체크

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231014
    /// </summary>
    /// <param name="id"></param>
    public GolemStatus(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.GolemTableData[(int)id]["ID"].ToString());
        WeakpointRate = float.Parse(Managers.Data.GolemTableData[(int)id]["WeakpointRate"].ToString());
        ActTime = float.Parse(Managers.Data.GolemTableData[(int)id]["ActTime"].ToString());
        Hp = int.Parse(Managers.Data.GolemTableData[(int)id]["Hp"].ToString());
        MoveSpeed = float.Parse(Managers.Data.GolemTableData[(int)id]["MoveSpeed"].ToString());
        Phase = int.Parse(Managers.Data.GolemTableData[(int)id]["Phase"].ToString());
    }

    /// <summary>
    /// 데미지 부여 함수
    /// 김민섭_231014
    /// </summary>
    /// <param name="dmg">받은 데미지</param>
    public void OnDamaged(int dmg)
    {
        Hp -= dmg;

        if (Hp <= 0)
        {   // 체력이 0 이하면 사망 처리
            Hp = 0;
            IsDie = true;
        }
    }

    public void OnDamaged(Status attacker)
    {

    }
}

#endregion

#region 유닛 관련 스테이터스

/// <summary>
/// 유닛 스탯 클래스
/// 231013_박시연
/// </summary>
public class UnitStatus : Status
{
    #region 프로퍼티 

    public float DurationTime { private set; get; }       // 배치 시간
    public int MaxCount { private set; get; }             // 최대 배치 개수
    public int Price { private set; get; }                // 구매 가격

    #endregion

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231014
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public UnitStatus(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.UnitTableData[(int)id]["ID"].ToString());
        DurationTime = float.Parse(Managers.Data.UnitTableData[(int)id]["DurationTime"].ToString());
        MaxCount = int.Parse(Managers.Data.UnitTableData[(int)id]["MaxCount"].ToString());
        Price = int.Parse(Managers.Data.UnitTableData[(int)id]["Price"].ToString());
    }
}

public class BulletStatus : Status
{
    #region 프로퍼티 

    public float Delay { private set; get; }          // 공격 간격
    public float Damage { private set; get; }         // 총알 공격력
    public float Speed { private set; get; }          // 총알 속도
    public float LifeTime { private set; get; }       // 총알 라이프 타임

    #endregion

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231014
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public BulletStatus(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.ProjectileTable[(int)id]["ID"].ToString());
        Delay = float.Parse(Managers.Data.ProjectileTable[(int)id]["Delay"].ToString());
        Damage = float.Parse(Managers.Data.ProjectileTable[(int)id]["Damage"].ToString());
        Speed = float.Parse(Managers.Data.ProjectileTable[(int)id]["Speed"].ToString());
        LifeTime = float.Parse(Managers.Data.ProjectileTable[(int)id]["LifeTime"].ToString());
    }

    /// <summary>
    /// 타겟에게 데미지를 주는 함수
    /// 231014_박시연
    /// </summary>
    public void OnDamaged(Collider target)
    {
        // TODO : 
        if (target.tag == "WeaknessPoint")
        {
            target.GetComponentInParent<Boss>().CurrStatus.OnDamaged(int.Parse(Damage.ToString()));
        }
        else if (target.tag == "Minion")
        {
            // 졸개 수정할 수도
            //target.GetComponent<Character>().SetHP(int.Parse(Damage.ToString()));
        }
    }
}

#endregion

#region 졸개 관련 스테이터스

public class MinionStatus : Status
{
    public int Type { private set; get; }           // 졸개의 종류 (1: Fast, 2: Power)
    public int Hp { private set; get; }             // 졸개의 체력
    public int Damage { private set; get; }         // 졸개의 데미지
    public float Speed { private set; get; }        // 졸개의 이동속도
    public float Range_Att { private set; get; }    // 졸개의 공격 사거리
    public float Range_Ex { private set; get; }     // 졸개의 폭발 범위

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231015
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public MinionStatus(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.MinionTableData[(int)id]["ID"].ToString());
        Type = int.Parse(Managers.Data.MinionTableData[(int)id]["Type"].ToString());
        Hp = int.Parse(Managers.Data.MinionTableData[(int)id]["Hp"].ToString());
        Damage = int.Parse(Managers.Data.MinionTableData[(int)id]["Damage"].ToString());
        Speed = float.Parse(Managers.Data.MinionTableData[(int)id]["Speed"].ToString());
        Range_Att = float.Parse(Managers.Data.MinionTableData[(int)id]["Range_Att"].ToString());
        Range_Ex = float.Parse(Managers.Data.MinionTableData[(int)id]["Range_Ex"].ToString());
    }
}

#endregion

#region 졸개 스폰 관련 데이터

public class MinionSpawn : Status
{
    public int Spawn_Condition { private set; get; }        // 졸개가 스폰되는 조건
    public float Spawn_Location { private set; get; }       // 졸개가 랜덤하게 스폰되는 위치
    public float Spawn_Time { private set; get; }           // 졸개가 스폰되는 간격
    public int Minion_Type1 { private set; get; }           // 스폰되는 졸개 타입1
    public int Type1_Amount { private set; get; }           // 스폰되는 타입1의 수량
    public int Minion_Type2 { private set; get; }           // 스폰되는 졸개 타입2
    public int Type2_Amount { private set; get; }           // 스폰되는 타입2의 수량
    public int Minion_Amount { private set; get; }          // 존재 가능한 졸개의 수량

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김민섭_231015
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public MinionSpawn(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["ID"].ToString());
        Spawn_Condition = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Spawn_Condition"].ToString());
        Spawn_Location = float.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Spawn_Location"].ToString());
        Spawn_Time = float.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Spawn_Time"].ToString());
        Minion_Type1 = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Minion_Type1"].ToString());
        Type1_Amount = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Type1_amount"].ToString());
        Minion_Type2 = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Minion_Type2"].ToString());
        Type2_Amount = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Type2_amount"].ToString());
        Minion_Amount = int.Parse(Managers.Data.MinionSpawnTableData[(int)id]["Minion_amount"].ToString());
    }
}

#endregion