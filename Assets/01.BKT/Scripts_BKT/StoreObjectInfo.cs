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
    static public Action CanBuy;

    public AudioClip purchaseSound;

    public int id = default;
    public string icon = default;
    public string itemName = default;
    public int price = default;
    public int time = default;
    public string description = default;

    private GameObject DescriptionUI;
    private Image baseImage;
    private GameObject outLineImage;
    private GameObject baseOutLineImage;
    private Image fillAmountImage;

    private Coroutine buyStartCoroutine;
    private bool isCanBuy;

    protected  virtual void Start()
    {
        DescriptionUI = GameObject.Find("Right_Item Description");
        outLineImage = transform.GetChild(2).gameObject;
        baseOutLineImage = transform.GetChild(1).gameObject;
        baseImage = transform.GetChild(0).GetComponent<Image>();
        fillAmountImage = transform.GetChild(3).GetComponent<Image>();
        baseImage.sprite = Resources.Load<Sprite>(icon);

        OtherOutLineOff += OnCursorPointUp; //생성시 커서 포인트 UP 함수를 이벤트에 추가
        CanBuy += CanBuyItem; //오브젝트가 구매 가능한지 여부 체크를 이벤트에 추가
        CanBuy();
    }

    private void OnDestroy()
    {

        OtherOutLineOff -= OnCursorPointUp;
    }

    //커서를 위에 올리면 실행되는 함수
    public void OnCursorPoint()
    {
        OtherOutLineOff(); // 이미지에 커서가 올라올 경우 다른 이미지의 아웃라인은 전부 해제

        DescriptionUI.SetActive(true);
        outLineImage.SetActive(true);
        baseOutLineImage.SetActive(false);

        DescriptionUI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(icon); // 아이콘 이름에 맞게 이미지 가져와서 넣음
        DescriptionUI.transform.GetChild(4).GetComponent<TMP_Text>().text = itemName; //아이템 이름 표시
        DescriptionUI.transform.GetChild(5).GetComponent<TMP_Text>().text = price.ToString(); // 가격 표시
        DescriptionUI.transform.GetChild(6).GetComponent<TMP_Text>().text = time.ToString(); //시간 표시
        DescriptionUI.transform.GetChild(8).GetComponent<TMP_Text>().text = description; // 설명 표시
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
        Debug.Log(transform.name);
        if(outLineImage == null)
        {
            outLineImage = transform.GetChild(2).gameObject;
        }
        if(baseOutLineImage == null) 
        {
            baseOutLineImage = transform.GetChild(1).gameObject;
        }
        outLineImage.SetActive(false);
        baseOutLineImage.SetActive(true);
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
        if(buyStartCoroutine != null) StopCoroutine(buyStartCoroutine);
        fillAmountImage.fillAmount = 0;
        CanBuy();
    }

    /// <summary>
    /// 유닛 구매 코루틴
    /// 배경택 _231016
    /// </summary>
    /// <returns></returns>
    IEnumerator BuyUnitCoroutine()
    {
        if (isCanBuy)
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
            Managers.MONEY.myMoney -= price;
            Managers.MONEY.ReflectMoney();
            Managers.Sound.Play(purchaseSound,Define.Sound.Sfx);
            CanBuy();

            // TODO: 유닛 생성
            if(isCanBuy)
            {
                CreateUnit();
            }
        }
        yield return null;
    }

    /// <summary>
    /// 유닛 구매 후 생성 함수
    /// 김민섭_231016
    /// </summary>
    protected virtual void CreateUnit()
    {
        Debug.Log("살수있어");
    }


    /// <summary>
    /// 아이템 구매 가능여부를 판단하는 함수
    /// 배경택 _231016
    /// </summary>
    public void CanBuyItem()
    {
        if(Managers.MONEY.myMoney < price)
        {
            fillAmountImage.fillAmount = 1;
            isCanBuy = false;
        }
        else
        {
            fillAmountImage.fillAmount = 0;
            isCanBuy = true;
        }
    }
}
