using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 골렘 투사체 데이터 테이블, 김민섭_231014
    public Dictionary<int, Dictionary<string, object>> Boss_MeteorStatusData { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    // 골렘 스탯 데이터 테이블, 김민섭_231014
    public Dictionary<int, Dictionary<string, object>> GolemTableData { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    // 유닛 스탯 데이터 테이블
    public Dictionary<int, Dictionary<string, object>> UnitTableData { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    // 총알 스탯 데이터 테이블
    public Dictionary<int, Dictionary<string, object>> ProjectileTable { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    // 졸개 스탯 데이터 테이블, 김민섭_231015
    public Dictionary<int, Dictionary<string, object>> MinionTableData { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    // 졸개 스폰 데이터 테이블, 김민섭_231015
    public Dictionary<int, Dictionary<string, object>> MinionSpawnTableData { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    // PC 스탯 데이터 테이블, 김재현_231016
    public Dictionary<int, Dictionary<string, object>> PCTableData { private set; get; } = new Dictionary<int, Dictionary<string, object>>();

    /// <summary>
    /// 모든 데이터 초기화 함수
    /// 김민섭_231014
    /// </summary>
    public void Init()
    {
        Boss_MeteorStatusData = CSVReader.ReadForDict("Data/Boss_MeteorStatus");
        GolemTableData = CSVReader.ReadForDict("Data/Golem_Table");
        UnitTableData = CSVReader.ReadForDict("Data/Unit_Table");
        ProjectileTable = CSVReader.ReadForDict("Data/Projectile_Table");
        MinionTableData = CSVReader.ReadForDict("Data/Minion_Table");
        MinionSpawnTableData = CSVReader.ReadForDict("Data/MinionSpawn_Table");
        PCTableData = CSVReader.ReadForDict("Data/PC_Table");
    }
}
