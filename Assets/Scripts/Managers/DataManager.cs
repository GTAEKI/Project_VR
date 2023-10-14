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

    /// <summary>
    /// 모든 데이터 초기화 함수
    /// 김민섭_231014
    /// </summary>
    public void Init()
    {
        Boss_MeteorStatusData = CSVReader.ReadForDict("Data/Boss_MeteorStatus");
        GolemTableData = CSVReader.ReadForDict("Data/Golem_Table");
        UnitTableData = CSVReader.ReadForDict("Data/Unit_Table");
    }
}
