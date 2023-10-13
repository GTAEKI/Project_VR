using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// 모든 유닛의 스폰 관리 클래스
public class UnitSpawner : UnitDataManager
{
    private GameObject unitType1;  // 유닛 1
    private GameObject unitType2;  // 유닛 2
    private int count = 0;         // 유닛 현재 생성 갯수

    private List<GameObject> unitList = new List<GameObject>();  // 생성할 유닛을 담을 List

    private void Start()
    {
        unitType1 = Resources.Load<GameObject>("Prefabs/UnitType1");
        unitType2 = Resources.Load<GameObject>("Prefabs/UnitType2");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SpawnUnit(unitType1,0,new Vector3(10,0,0)); // 1번 유닛 생성
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SpawnUnit(unitType2,1,new Vector3(0,0,0));  // 2번 유닛 생성
        }
    }

    /// <summary>
    /// 유닛 생성 함수
    /// 231013_박시연
    /// </summary>
    /// <param name="createObj">생성할 유닛</param>
    /// <param name="index">유닛 번호</param>
    /// <param name="spawnPos">생성 좌표</param>
    public void SpawnUnit( GameObject createObj,int index, Vector3 spawnPos)
    {
        if (int.Parse(unitDatas[index]["MaxCount"].ToString()) == count)
        {
            return;
        }

        // 유닛을 좌표에 생성한다.
        GameObject instance = Instantiate(createObj, spawnPos , Quaternion.identity);
        unitList.Add(instance);  // 생성한 유닛을 관리하기 쉽게 List에 넣어준다.
        count++;

        StartCoroutine(DelayDestory(index,instance));
    }

    /// <summary>
    /// 생성시간에 따른 유닛 제거 코루틴
    /// </summary>
    /// <param name="index">유닛 번호</param>
    /// <param name="instance">생성한 유닛</param>
    /// <returns></returns>
    public IEnumerator DelayDestory(int index, GameObject instance)
    {
        float durationTime = float.Parse(unitDatas[index]["DurationTime"].ToString());

        while (true)
        {
            durationTime -= Time.deltaTime;

            if(durationTime <= 0)
            {
                Destroy(instance);
                count--;
                yield break;
            }

            yield return null;
        }
    }
}
