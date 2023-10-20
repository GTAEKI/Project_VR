using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject startEndUI;
    GameObject store;
    StartEndUI startEndUIText;
    Boss bossScript;

    public bool isGameOver;
    public bool isVictory;

    public void Init()
    {
        Managers.MONEY.Init(); // 배경택_231018
        startEndUI = GameObject.Find("StartEndUI/Panel");
        store = GameObject.Find("Store");

        startEndUIText = startEndUI.GetComponent<StartEndUI>();

        isGameOver = false;
        isVictory = false;

    }

    public void SearchBoss()
    {
        bossScript = GameObject.Find("Boss_Golem").GetComponent<Boss>();

    }

    public void ClickGameStart()
    {
        if (startEndUI == null)
        {
            startEndUI = GameObject.Find("StartEndUI/Panel");
        }
        if (store == null)
        {
            store = GameObject.Find("Store");
        }

        startEndUI.SetActive(false);
        store.SetActive(true);
        bossScript.StartCoroutineDelayIdle();
    }

    public void CloseStartEndUI()
    {
        startEndUI.SetActive(false);
    }


    private void ReadyScene()
    {
        startEndUIText.InsertTextStart();
        startEndUI.SetActive(true);
    }

    public void GameOverLose()
    {
        startEndUI.SetActive(true);
        startEndUIText.InsertTextLose();
        isGameOver = true;
        isVictory = false;
    }

    public void GameOverVictory()
    {
        startEndUI.SetActive(true);
        startEndUIText.InsertTextVictory();
        isGameOver = true;
        isVictory = true;
    }



}
