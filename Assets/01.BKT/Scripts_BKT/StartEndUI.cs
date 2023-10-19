using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartEndUI : MonoBehaviour
{

    TMP_Text bigText;
    TMP_Text leftSmallText;
    TMP_Text rightSmallText;

    [Header("When GameStart")]
    [SerializeField]
    private string start_BigText = "Defence to Monster";
    [SerializeField]
    private string start_Left_SmallText = "Start";
    [SerializeField]
    private string start_Right_SmallText = "End";

    [Header("When Victory")]
    [SerializeField]
    private string victory_BigText = "Victory";
    [SerializeField]
    private string victory_Left_SmallText = "Restart";
    [SerializeField]
    private string victory_Right_SmallText = "End";

    [Header("When Lose")]
    [SerializeField]
    private string gameOver_BigText = "GameOver";
    [SerializeField]
    private string gameOver_Left_SmallText = "Restart";
    [SerializeField]
    private string gameOver_Right_SmallText = "End";

    public void Init()
    {
        bigText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        leftSmallText = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        rightSmallText = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        

        //bigText.text = start_BigText;
        //leftSmallText.text = start_Left_SmallText;
        //rightSmallText.text = start_Right_SmallText;
    }


    /// <summary>
    /// 게임 시작시 텍스트 삽입
    /// 배경택 _231018
    /// </summary>
    public void InsertTextStart()
    {
        bigText.text = start_BigText;
        leftSmallText.text = start_Left_SmallText;
        rightSmallText.text = start_Right_SmallText;
    }

    /// <summary>
    /// 게임 승리시 텍스트 삽입
    /// 배경택 _231018
    /// </summary>
    public void InsertTextVictory()
    {
        bigText.text = victory_BigText;
        leftSmallText.text = victory_Left_SmallText;
        rightSmallText.text = victory_Right_SmallText;
    }

    /// <summary>
    /// 게임 패배시 텍스트 삽입
    /// 배경택 _231018
    /// </summary>
    public void InsertTextLose()
    {
        bigText.text = gameOver_BigText;
        leftSmallText.text = gameOver_Left_SmallText;
        rightSmallText.text = gameOver_Right_SmallText;
    }
}
