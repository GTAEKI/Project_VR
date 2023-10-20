using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject startEndUI;
    GameObject store;
    StartEndUI startEndUIText;
    Boss bossScript;

    public void Init()
    {
        Managers.MONEY.Init(); // 배경택_231018
        startEndUI = GameObject.Find("StartEndUI/Panel");
        store = GameObject.Find("Store");

        startEndUIText = startEndUI.GetComponent<StartEndUI>();
        store.SetActive(false);

    }

    public void SearchBoss()
    {
        bossScript = GameObject.Find("Boss_Golem").GetComponent<Boss>();

    }

    public void ClickGameStart()
    {
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
