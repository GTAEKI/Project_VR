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
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
