using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void ClickStartButton()
    {
        Managers.GameManager.ClickGameStart();
    }

    public void ClickReStartButton()
    {
        // TODO: 재시작시 실행될 내용 추가
    }
}
