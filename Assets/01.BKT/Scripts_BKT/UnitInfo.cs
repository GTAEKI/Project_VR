using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : StoreObjectInfo
{
    // Buff와 다른거 생기면 추가
    protected override void CreateUnit()
    {
        base.CreateUnit();

        UnitSpawner spawner = GameObject.Find("UnitSpawnPoint").GetComponent<UnitSpawner>();
        if(spawner != null )
        {
            if (id == 0) spawner.SpawnUnit(spawner.MinionUnit, (int)Define.Data_ID_List.Unit_Minion);
            else if (id == 1) spawner.SpawnUnit(spawner.GolemUnit, (int)Define.Data_ID_List.Unit_Golem);
        }
    }
}
