using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDataManager : MonoBehaviour
{
    public List<Dictionary<string, object>> bulletDatas = new List<Dictionary<string, object>>();  // csv 데이터 불러오기
    private void Awake()
    {
        bulletDatas = CSVReader.Read("Data/Bullet");  // 데이터 세팅
    }
}
