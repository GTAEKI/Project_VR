using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataManager : MonoBehaviour
{
    public List<Dictionary<string, object>> unitDatas = new List<Dictionary<string, object>>();  // csv 데이터 불러오기
    private void Awake()
    {
        unitDatas = CSVReader.Read("Data/Unit_Table");  // 데이터 세팅
    }
}
