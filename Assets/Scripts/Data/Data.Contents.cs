using System;
using System.ComponentModel;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 모든 기본 스탯 클래스
/// 김민섭_231014
/// </summary>
[Serializable]
public class Status
{
    [SerializeField]
    [Tooltip("고유 번호")] private int id;

    public int ID { protected set => id = value; get => id; }         // 고유 번호
}

#region 보스 관련 스테이터스

[Serializable]
/// <summary>
/// 메테오 스탯 클래스
/// 김민섭_231014
/// </summary>
public class MeteorStatus: Status
{
    [SerializeField]
    [Tooltip("사망 체크")] private bool isDie;
    [Space]
    [SerializeField]
    [Tooltip("투사체 체력")] private int hp;
    [SerializeField]
    [Tooltip("투사체 활성화 시간")] private float durationTime;
    [SerializeField]
    [Tooltip("투사체 피해량")] private int damage;
    [SerializeField]
    [Tooltip("투사체 이동속도")] private float speed;

    // 일반 데이터
    public bool IsDie { private set => isDie = value; get => isDie; }                             // 사망 체크
    public int Hp { private set => hp = value; get => hp; }                                     // 투사체 체력
    public float DurationTime { private set => durationTime = value; get => durationTime; }     // 투사체 활성화 시간
    public int Damage { private set => damage = value; get => damage; }                         // 투사체 피해량
    public float Speed { private set => speed = value; get => speed; }                          // 투사체 이동속도

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
    public void OnDamaged(int dmg)
    {
        // TODO: attacker의 스탯 클래스 접근
        // TODO: 데미지 공식에 맞춰서 체력 감소 시킴
        Hp -= dmg;

        if(Hp <= 0)
        {   // 체력이 0 이하이면 사망
            Hp = 0;
            IsDie = false;
        }
    }
}

[Serializable]
/// <summary>
/// 골렘 스탯 클래스
/// 김민섭_231014
/// </summary>
public class GolemStatus : Status
{
    [SerializeField]
    [Tooltip("그로기 체크")] private bool isGroggy;
    [SerializeField]
    [Tooltip("사망 체크")] private bool isDie;
    [Space]
    [SerializeField]
    [Tooltip("약점 피격 시 받는 데미지 증가율")] private float weakpointRate;
    [SerializeField]
    [Tooltip("비활성화된 약점이 다시 활성화될 때까지 소요되는 시간")] private float actTime;
    [SerializeField]
    [Tooltip("골렘 체력")] private int hp;
    [SerializeField]
    [Tooltip("골렘 이동속도")] private float moveSpeed;
    [SerializeField]
    [Tooltip("페이지")] private int phase;

    // 일반 데이터
    public bool IsGroggy { set => isGroggy = value; get => isGroggy; }                    // 그로기 체크
    public bool IsDie { private set => isDie = value; get => isDie; }                             // 사망 체크

    // 테이블 데이터
    public float WeakpointRate { private set => weakpointRate = value; get => weakpointRate; }    // 약점 피격 시 받는 데미지 증가율
    public float ActTime { private set => actTime = value; get => actTime; }                      // 비활성화된 약점이 다시 활성화될 때까지 소요되는 시간
    public int Hp { private set => hp = value; get => hp; }                                       // 골렘 체력
    public float MoveSpeed { private set => moveSpeed = value; get => moveSpeed; }                // 골렘 이동속도
    public int Phase { private set => phase = value; get => phase; }                              // 페이지

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

        if(Hp % 10 == 0)
        {
            IsGroggy = true;
        }

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

[Serializable]
/// <summary>
/// 유닛 스탯 클래스
/// 231013_박시연
/// </summary>
public class UnitStatus : Status
{
    [SerializeField]
    [Tooltip("배치 시간")] private float durationTime;
    [SerializeField]
    [Tooltip("최대 배치 개수")] private int maxCount;
    [SerializeField]
    [Tooltip("구매 가격")] private int price;

    #region 프로퍼티 

    public float DurationTime { private set => durationTime = value; get => durationTime; }       // 배치 시간
    public int MaxCount { private set => maxCount = value; get => maxCount; }             // 최대 배치 개수
    public int Price { private set => price = value; get => price; }                // 구매 가격

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

[Serializable]
public class BulletStatus : Status
{
    [SerializeField]
    [Tooltip("공격 간격")] private float delay;
    [SerializeField]
    [Tooltip("총알 공격력")] private float damage;
    [SerializeField]
    [Tooltip("총알 속도")] private float speed;
    [SerializeField]
    [Tooltip("총알 라이프 타임")] private float lifeTime;

    #region 프로퍼티 

    public float Delay { private set => delay = value; get => delay; }                // 공격 간격
    public float Damage { private set => damage = value; get => damage; }             // 총알 공격력
    public float Speed { private set => speed = value; get => speed; }                // 총알 속도
    public float LifeTime { private set => lifeTime = value; get => lifeTime; }       // 총알 라이프 타임

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
            Boss boss = target.GetComponentInParent<Boss>();

            boss.CurrStatus.OnDamaged((int)(Damage * (1f + boss.CurrStatus.WeakpointRate)));

            // 데미지에 비례하여 금액 추가, 김민섭_231016
            Managers.MONEY.BossHitMoney((int)(Damage * (1f + boss.CurrStatus.WeakpointRate)));
            Managers.MONEY.ReflectMoney();

            #region 데미지 출력
            Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
            TextMeshProUGUI textDamage;  // 데미지 출력 Text
            textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 currentPos = target.transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
            GameObject.Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
            canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

            textDamage.text = $"{(int)(Damage * (1f + boss.CurrStatus.WeakpointRate))}";  // Text에 데미지가 보여지게 한다.
            #endregion
        }
        else if (target.tag == "CenterPoint")
        {
            Boss boss = target.GetComponentInParent<Boss>();

            boss.CurrStatus.OnDamaged((int)Damage);

            // 데미지에 비례하여 금액 추가, 김민섭_231016
            Managers.MONEY.BossHitMoney((int)Damage);
            Managers.MONEY.ReflectMoney();

            #region 데미지 출력
            Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
            TextMeshProUGUI textDamage;  // 데미지 출력 Text
            textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 currentPos = target.transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
            GameObject.Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
            canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

            textDamage.text = $"{(int)(Damage)}";  // Text에 데미지가 보여지게 한다.
            #endregion
        }
        else if (target.tag == "Minion")
        {   // 졸개 공격 데미지 부여 추가, 김민섭_231016
            MinionController minion = target.GetComponent<MinionController>();

            minion.CurrStatus.OnDamaged((int)(Damage));

            #region 데미지 출력
            Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
            TextMeshProUGUI textDamage;  // 데미지 출력 Text
            textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 currentPos = target.transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
            GameObject.Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
            canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

            textDamage.text = $"{(int)Damage}";  // Text에 데미지가 보여지게 한다.
            #endregion
        }
        else if(target.tag == "Meteor")
        {   // 메테오 공격 데미지 부여 추가, 김민섭_231017
            MeteorController meteor = target.GetComponent<MeteorController>();

            meteor.Status.OnDamaged((int)Damage);

            #region 데미지 출력
            Canvas canvasDamage = Resources.Load("Prefabs/UI/DamageCanvas").GetComponent<Canvas>();
            TextMeshProUGUI textDamage;  // 데미지 출력 Text
            textDamage = canvasDamage.transform.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 currentPos = target.transform.position + new Vector3(0f, 10f, -5f);        // 현재 위치
            GameObject.Instantiate(canvasDamage, currentPos, Quaternion.identity);  // 현재 위치에 Canvas 생성
            canvasDamage.worldCamera = Camera.main;                    // Canvas Camera Setting

            textDamage.text = $"{(int)Damage}";  // Text에 데미지가 보여지게 한다.
            #endregion
        }
    }
}

#endregion

#region 졸개 관련 스테이터스

[Serializable]
public class MinionStatus : Status
{
    [SerializeField]
    [Tooltip("사망 체크")] private bool isDie;
    [Space]
    [SerializeField]
    [Tooltip("졸개의 종류 (1: Fast, 2: Power)")] private int type;
    [SerializeField]
    [Tooltip("졸개의 체력")] private int hp;
    [SerializeField]
    [Tooltip("졸개의 데미지")] private int damage;
    [SerializeField]
    [Tooltip("졸개의 이동속도")] private float speed;
    [SerializeField]
    [Tooltip("졸개의 공격 사거리")] private float range_Att;
    [SerializeField]
    [Tooltip("졸개의 폭발 범위")] private float range_Ex;

    // 일반 데이터
    public bool IsDie { private set => isDie = value; get => isDie; }                             // 사망 체크

    public int Type { private set => type = value; get => type; }                   // 졸개의 종류 (1: Fast, 2: Power)
    public int Hp { private set => hp = value; get => hp; }                         // 졸개의 체력
    public int Damage { private set => damage = value; get => damage; }             // 졸개의 데미지
    public float Speed { private set => speed = value; get => speed; }              // 졸개의 이동속도
    public float Range_Att { private set => range_Att = value; get => range_Att; }  // 졸개의 공격 사거리
    public float Range_Ex { private set => range_Ex = value; get => range_Ex; }     // 졸개의 폭발 범위

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

    /// <summary>
    /// 데미지 부여 함수
    /// 김민섭_231016
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
}

#endregion

#region 졸개 스폰 관련 데이터

[Serializable]
public class MinionSpawn : Status
{
    [SerializeField]
    [Tooltip("졸개가 스폰되는 조건")] private int spawn_condition;
    [SerializeField]
    [Tooltip("졸개가 랜덤하게 스폰되는 위치")] private float spawn_Location;
    [SerializeField]
    [Tooltip("졸개가 스폰되는 간격")] private float spawn_Time;
    [SerializeField]
    [Tooltip("스폰되는 졸개 타입1")] private int minion_Type1;
    [SerializeField]
    [Tooltip("스폰되는 타입1의 수량")] private int type1_Amount;
    [SerializeField]
    [Tooltip("스폰되는 졸개 타입2")] private int minion_Type2;
    [SerializeField]
    [Tooltip("스폰되는 타입2의 수량")] private int type2_Amount;
    [SerializeField]
    [Tooltip("존재 가능한 졸개의 수량")] private int minion_Amount;

    public int Spawn_Condition { private set => spawn_condition = value; get => spawn_condition; }      // 졸개가 스폰되는 조건
    public float Spawn_Location { private set => spawn_Location = value; get => spawn_Location; }       // 졸개가 랜덤하게 스폰되는 위치
    public float Spawn_Time { private set => spawn_Time = value; get => spawn_Time; }                   // 졸개가 스폰되는 간격
    public int Minion_Type1 { private set => minion_Type1 = value; get => minion_Type1; }               // 스폰되는 졸개 타입1
    public int Type1_Amount { private set => type1_Amount = value; get => type1_Amount; }               // 스폰되는 타입1의 수량
    public int Minion_Type2 { private set => minion_Type2 = value; get => minion_Type2; }               // 스폰되는 졸개 타입2
    public int Type2_Amount { private set => type2_Amount = value; get => type2_Amount; }               // 스폰되는 타입2의 수량
    public int Minion_Amount { private set => minion_Amount = value; get => minion_Amount; }            // 존재 가능한 졸개의 수량

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

#region PC 관련 스테이터스

[Serializable]
/// <summary>
/// PC 스탯 클래스
/// 231016_김재현
/// </summary>
public class PCStatus : Status
{
    [SerializeField]
    [Tooltip("사망 체크")] private bool isDie;
    [Space]
    [SerializeField]
    [Tooltip(" 최대 체력 ")] private int maxHp;
    [SerializeField]
    [Tooltip("타입")] private int type;

    #region 프로퍼티 

    // 일반 데이터
    public bool IsDie { private set => isDie = value; get => isDie; }                             // 사망 체크

    public int MaxHp { private set => maxHp = value; get => maxHp; }       // 최대 체력
    public int Type { private set => type = value; get => type; }             // 타입

    #endregion

    /// <summary>
    /// 데이터 아이디로 세팅하는 생성자
    /// 김재현_231016
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public PCStatus(Define.Data_ID_List id)
    {
        ID = int.Parse(Managers.Data.PCTableData[(int)id]["ID"].ToString());
        MaxHp = int.Parse(Managers.Data.PCTableData[(int)id]["MaxHp"].ToString());
        Type = int.Parse(Managers.Data.PCTableData[(int)id]["Type"].ToString());
    }

    /// <summary>
    /// 플레이어에게 피해를 입히는 함수
    /// 김민섭_231017
    /// </summary>
    /// <param name="currHp">현재 체력</param>
    /// <param name="dmg">받는 데미지</param>
    public void OnDamaged(ref int currHp, int dmg)
    {
        KJHPlayer player = GameObject.FindObjectOfType<KJHPlayer>();
        player?.PlayDamageEffect();

        currHp -= dmg;

        if(currHp <=0)
        {
            currHp = 0;
            IsDie = true;
        }
    }
}
#endregion