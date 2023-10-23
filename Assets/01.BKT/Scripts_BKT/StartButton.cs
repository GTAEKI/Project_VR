using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    public void ClickStartButton()
    {
        Managers.MONEY.Init(); // 배경택_231018
        StoreObjectInfo.CanBuy(); // 배경택_231023
        Managers.Camera.Init(); //배경택_231023
        Managers.Camera.StartCameraSet(); //배경택_231023
        Managers.GameManager.ClickGameStart();
        Managers.GameManager.isGameOver = false;
    }

    public void ClickReStartButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
        Managers.GameManager.isGameOver = false;

    }
}
