using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minon : UnitController
{
    protected override void Init()
    {
        base.Init();

        baseUnitStatus = new BaseUnitStatus(
            int.Parse(unitDataManager.unitDatas[0]["ID"].ToString()),
            unitDataManager.unitDatas[0]["Type"].ToString(),
            float.Parse(unitDataManager.unitDatas[0]["DurationTime"].ToString()),
            int.Parse(unitDataManager.unitDatas[0]["MaxCount"].ToString()),
            int.Parse(unitDataManager.unitDatas[0]["Price"].ToString())
            );

        currentUnitStatus = new CurrentUnitStatus(baseUnitStatus);

        Debug.Log($"ID : {currentUnitStatus.ID}\nType : {currentUnitStatus.Type}" +
            $"\nDurationTime : {currentUnitStatus.DurationTime}\nMaxCount : {currentUnitStatus.MaxCount}\nPrice : {currentUnitStatus.Price}");
    }
}
