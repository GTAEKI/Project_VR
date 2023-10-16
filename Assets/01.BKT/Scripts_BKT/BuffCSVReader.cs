using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCSVReader : MonoBehaviour
{
    public GameObject ItemUIObjectPrefab;

    private List<Dictionary<string, object>> buffCSV = new List<Dictionary<string, object>>();

    void Start()
    {        
        buffCSV = CSVReader_KT.Read("BuffCSV");

        // CSV 정보를 BuffInfo의 리스트에 읽어들입니다.
        for(int i = 0; i < buffCSV.Count; i++)
        {            
            GameObject uiObject = Instantiate(ItemUIObjectPrefab, this.transform); // 내 하위 오브젝트로 생성

            //Debug.Log(buffCSV[i]["BuffID"].ToString() + buffCSV[i]["BuffIcon"].ToString() + buffCSV[i]["BuffName"].ToString() + buffCSV[i]["BuffPrice"].ToString()
            //    + buffCSV[i]["BuffTime"].ToString() + buffCSV[i]["BuffDescription"].ToString());

            // 읽은 정보를 각각의 오브젝트에 넣어줌
            uiObject.GetComponent<BuffInfo>().id = int.Parse(buffCSV[i]["BuffID"].ToString());
            uiObject.GetComponent<BuffInfo>().icon = buffCSV[i]["BuffIcon"].ToString();
            uiObject.GetComponent<BuffInfo>().itemName = buffCSV[i]["BuffName"].ToString();
            uiObject.GetComponent<BuffInfo>().price = int.Parse(buffCSV[i]["BuffPrice"].ToString());
            uiObject.GetComponent<BuffInfo>().time = int.Parse(buffCSV[i]["BuffTime"].ToString());
            uiObject.GetComponent<BuffInfo>().description = buffCSV[i]["BuffDescription"].ToString();
        }
    }
}
