using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCSVReader : MonoBehaviour
{

    public GameObject ItemUIObjectPrefab;

    private List<Dictionary<string, object>> unitCSV = new List<Dictionary<string, object>>();

    void Start()
    {
        unitCSV = CSVReader_KT.Read("UnitCSV");

        // CSV 정보를 UnitInfo의 리스트에 읽어들입니다.
        for (int i = 0; i < unitCSV.Count; i++)
        {
            GameObject uiObject = Instantiate(ItemUIObjectPrefab, this.transform); // 내 하위 오브젝트로 생성

            //Debug.Log(unitCSV[i]["UnitID"].ToString() + unitCSV[i]["UnitIcon"].ToString() + unitCSV[i]["UnitName"].ToString() + unitCSV[i]["UnitPrice"].ToString()
            //    + unitCSV[i]["UnitTime"].ToString() + unitCSV[i]["UnitDescription"].ToString());

            // 읽은 정보를 각각의 오브젝트에 넣어줌
            uiObject.GetComponent<UnitInfo>().id = int.Parse(unitCSV[i]["UnitID"].ToString());
            uiObject.GetComponent<UnitInfo>().icon = unitCSV[i]["UnitIcon"].ToString();
            uiObject.GetComponent<UnitInfo>().itemName = unitCSV[i]["UnitName"].ToString();
            uiObject.GetComponent<UnitInfo>().price = int.Parse(unitCSV[i]["UnitPrice"].ToString());
            uiObject.GetComponent<UnitInfo>().time = int.Parse(unitCSV[i]["UnitTime"].ToString());
            uiObject.GetComponent<UnitInfo>().description = unitCSV[i]["UnitDescription"].ToString();
        }
    }
}
