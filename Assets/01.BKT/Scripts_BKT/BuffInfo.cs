using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo : StoreObjectInfo
{
    // Unit과 다른거 생기면 추가
    GameObject rightHandAim;

    protected override void Start()
    {
        base.Start();
        rightHandAim = GameObject.Find("Right_Hand");
    }

    protected override void CreateUnit()
    {
        base.CreateUnit();

        StartCoroutine(ReflectBuff());
    }


    // 버프 일정시간 적용
    IEnumerator ReflectBuff()
    {
        rightHandAim.GetComponent<Aim>().isPotion = true;

        yield return new WaitForSeconds(time);

        rightHandAim.GetComponent<Aim>().isPotion = false;
        Debug.Log("강화 총알 꺼짐");

    }


}