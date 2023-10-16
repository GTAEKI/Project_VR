using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoreObjectInfo : MonoBehaviour
{
    static public Action OtherOutLineOff; // 선택되지 않은 이미지의 아웃라인 off를 위한 이벤트

    public int id = default;
    public string icon = default;
    public string itemName = default;
    public int price = default;
    public int time = default;
    public string description = default;

    private GameObject DescriptionUI;
    private Image baseImage;
    private GameObject outLineImage;
    private Image fillAmountImage;

    private Coroutine buyStartCoroutine;

    private void Start()
    {
        DescriptionUI = GameObject.Find("Right_Item Description");
        outLineImage = transform.GetChild(0).gameObject;
        baseImage = transform.GetChild(1).GetComponent<Image>();
        fillAmountImage = transform.GetChild(2).GetComponent<Image>();
        baseImage.sprite = Resources.Load<Sprite>(icon);

        OtherOutLineOff += OnCursorPointUp; //생성시 커서 포인트 UP 함수를 이벤트에 추가
    }

    //커서를 위에 올리면 실행되는 함수
    public void OnCursorPoint()
    {
        OtherOutLineOff(); // 이미지에 커서가 올라올 경우 다른 이미지의 아웃라인은 전부 해제

        DescriptionUI.SetActive(true);
        outLineImage.SetActive(true);

        DescriptionUI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(icon); // 아이콘 이름에 맞게 이미지 가져와서 넣음
        DescriptionUI.transform.GetChild(3).GetComponent<TMP_Text>().text = itemName; //아이템 이름 표시
        DescriptionUI.transform.GetChild(4).GetComponent<TMP_Text>().text = price.ToString(); // 가격 표시
        DescriptionUI.transform.GetChild(5).GetComponent<TMP_Text>().text = time.ToString(); //시간 표시
        DescriptionUI.transform.GetChild(7).GetComponent<TMP_Text>().text = description; // 설명 표시
    }

    public void CursorPointDown()
    {
        
    }

    /// <summary>
    /// 커서가 띄어졌을때 호출되는 함수
    /// 배경택
    /// </summary>
    public void OnCursorPointUp()
    {
        outLineImage.SetActive(false);
    }

    /// <summary>
    /// 유닛 구매 함수
    /// 배경택 _ 231016
    /// </summary>
    public void BuyUnit()
    {
        buyStartCoroutine = StartCoroutine(BuyUnitCoroutine());
    }


    /// <summary>
    /// 유닛 구매 취소 함수
    /// 배경택 _231016
    /// </summary>
    public void DontBuyUnit()
    {
        StopCoroutine(buyStartCoroutine);
        fillAmountImage.fillAmount = 0;
    }

    /// <summary>
    /// 유닛 구매 코루틴
    /// 배경택 _231016
    /// </summary>
    /// <returns></returns>
    IEnumerator BuyUnitCoroutine()
    {
        Debug.Log("구매 코루틴 진입");
        while(fillAmountImage.fillAmount < 1)
        {
            Debug.Log("구매 while 진입");
            fillAmountImage.fillAmount += 1 * Time.deltaTime;

            yield return null;
        }

        fillAmountImage.fillAmount = 0; // 구매 완료되었으므로 초기화 

        //TODO 아이템 구매
    }
}
