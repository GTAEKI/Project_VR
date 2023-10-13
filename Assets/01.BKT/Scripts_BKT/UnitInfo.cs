using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    private List<Dictionary<string, object>> unitCSV = default;

    public UnitInfo()
    {
        
    }

    void Start()
    {
        unitCSV = CSVReader_KT.Read("UnitCSV");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
