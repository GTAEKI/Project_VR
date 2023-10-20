using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void ClickStartButton()
    {
        Managers.GameManager.ClickGameStart();
        Managers.GameManager.isGameOver = false;
    }

    public void ClickReStartButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
        Managers.GameManager.isGameOver = false;

    }
}
