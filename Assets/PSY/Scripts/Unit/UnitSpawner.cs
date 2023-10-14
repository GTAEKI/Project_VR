using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// TODO : 플레이어 양쪽에 순서대로 왼쪽먼저 유닛 생성
// 모든 유닛의 스폰 관리 클래스
public class UnitSpawner : UnitDataManager
{
    private GameObject unitType1;  // 유닛 1
    private GameObject unitType2;  // 유닛 2
    private int count = 0;         // 유닛 현재 생성 갯수

    private GameObject player;     // 플레이어

    private Vector3[] spawnPosArr = new Vector3[2];  // 생성 위치 배열 
    private GameObject[] units = new GameObject[2];  // 유닛 배열

    private void Start()
    {
        unitType1 = Resources.Load<GameObject>("Prefabs/Unit/UnitType1");
        unitType2 = Resources.Load<GameObject>("Prefabs/Unit/UnitType2");

        player = GameObject.Find("Player");

        spawnPosArr[0] = player.transform.position + new Vector3(10f, 0f, 5f);
        spawnPosArr[1] = player.transform.position + new Vector3(-10f, 0f, 5f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnUnit(unitType1, 0); // 1번 유닛 생성
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SpawnUnit(unitType2, 1);  // 2번 유닛 생성
        }
    }

    /// <summary>
    /// 유닛 생성 함수
    /// 231013_박시연
    /// </summary>
    /// <param name="createObj">생성할 유닛</param>
    /// <param name="index">유닛 번호</param>
    /// <param name="spawnPos">생성 좌표</param>
    public void SpawnUnit(GameObject createObj, int index)
    {
        if (int.Parse(unitDatas[index]["MaxCount"].ToString()) == count)
        {
            return;
        }

        

        for (int i = 0; i < units.Length; i++)
        {
            if (units[i] == null)
            {
                // 유닛을 좌표에 생성한다.
                GameObject instance = Instantiate(createObj, spawnPosArr[i], Quaternion.identity);
                units[i] = instance;  // 생성한 유닛을 관리하기 쉽게 List에 넣어준다.
                count++;
                StartCoroutine(DelayDestory(index, instance));

                break;
            }

        }

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

            if (durationTime <= 0)
            {
                Destroy(instance);
                count--;
                yield break;
            }

            yield return null;
        }
    }
}
