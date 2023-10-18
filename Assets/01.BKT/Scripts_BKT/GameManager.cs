using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject startEndUI;
    StartEndUI startEndUIText;
    

    public void Init()
    {
        Managers.MONEY.Init(); // 배경택_231018
        startEndUI = GameObject.Find("StartEndUI/Panel");
        startEndUIText = startEndUI.GetComponent<StartEndUI>();
        startEndUI.SetActive(false);
        //ReadyScene();
    }


    private void ReadyScene()
    {
        startEndUIText.InsertTextStart();
        startEndUI.SetActive(true);
    }

    private void GameOverLose()
    {
        startEndUIText.InsertTextLose();
        startEndUI.SetActive(true);
    }

    private void GameOverVictory()
    {
        startEndUIText.InsertTextVictory();
        startEndUI.SetActive(true);
    }



}
