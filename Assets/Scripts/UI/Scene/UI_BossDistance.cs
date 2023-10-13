using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_BossDistance : UI_Scene
{
    private enum Texts
    {
        TM_Distance
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    /// <summary>
    /// 거리 표기 텍스트 세팅 함수
    /// 김민섭_231013
    /// </summary>
    /// <param name="currDistance">현재 거리</param>
    public void SetDistanceText(float currDistance)
    {
        GetTMP((int)Texts.TM_Distance).text = $"Distance\n{string.Format("{0:F1}", currDistance)} m";
    }
}
