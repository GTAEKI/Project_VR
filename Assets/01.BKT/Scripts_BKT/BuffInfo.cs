using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo : MonoBehaviour
{
    List<int> id = new List<int>();
    List<string> itemName = new List<string>();
    List<int> price = new List<int>();
    List<string> description = new List<string>();


    private List<Dictionary<string, object>> buffCSV = new List<Dictionary<string, object>>();

    void Start()
    {        
        buffCSV = CSVReader_KT.Read("BuffCSV");

        // CSV 정보를 BuffInfo의 리스트에 읽어들입니다.
        for(int i = 0; i < buffCSV.Count; i++)
        {
            
            Debug.Log(buffCSV[i]["ID"].ToString() + buffCSV[i]["Buff"].ToString() + buffCSV[i]["Price"].ToString() + buffCSV[i]["Description"].ToString());

            id.Add(int.Parse(buffCSV[i]["ID"].ToString()));
            itemName.Add(buffCSV[i]["Buff"].ToString());
            price.Add(int.Parse(buffCSV[i]["Price"].ToString()));
            description.Add(buffCSV[i]["Description"].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
