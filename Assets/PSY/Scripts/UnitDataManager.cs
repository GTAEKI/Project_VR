using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataManager : MonoBehaviour
{
    private void Start()
    {
        List<Dictionary<string, object>> unitDatas = CSVReader.Read("Data/Unit");

        for (var i = 0; i < unitDatas.Count; i++)
        {
            Debug.Log("ID : " + unitDatas[i]["ID"] + " Info : " + unitDatas[i]["Info"] +
                                                      " Type : " + unitDatas[i]["Type"] +
                                                      " Damage : " + unitDatas[i]["Damage"] +
                                                      " DurationTime : " + unitDatas[i]["DurationTime"] +
                                                      " MaxCount : " + unitDatas[i]["MaxCount"] +
                                                      " Price : " + unitDatas[i]["Price"]
                                                      );
        }
    }

    public void SetUnitData(  )
    {
        
    }
}
