using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : UnitController
{
    protected override void Init()
    {
        base.Init();

        baseStatus = new BaseStatus(
            int.Parse(unitDataManager.unitDatas[1]["ID"].ToString()),
            unitDataManager.unitDatas[1]["Type"].ToString(),
            float.Parse(unitDataManager.unitDatas[1]["Damage"].ToString()),
            float.Parse(unitDataManager.unitDatas[1]["DurationTime"].ToString()),
            int.Parse(unitDataManager.unitDatas[1]["MaxCount"].ToString()),
            int.Parse(unitDataManager.unitDatas[1]["Price"].ToString())
            );

        currentStatus = new CurrentStatus(baseStatus);

        Debug.Log($"ID : {currentStatus.ID}\nType : {currentStatus.Type}\nDamage : {currentStatus.Damage}"+
            $"\nDurationTime : {currentStatus.DurationTime}\nMaxCount : {currentStatus.MaxCount}\nPrice : {currentStatus.Price}");
    }
}
