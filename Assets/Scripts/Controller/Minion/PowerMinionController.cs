using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMinionController : MinionController
{
    protected override void Init()
    {
        currStatus = new MinionStatus(Define.Data_ID_List.Minion_Power);
    }
}
