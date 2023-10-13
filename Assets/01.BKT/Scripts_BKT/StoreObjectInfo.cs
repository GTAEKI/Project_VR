using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreObjectInfo : MonoBehaviour
{

    public int id = default;
    public string icon = default;
    public string itemName = default;
    public int price = default;
    public int time = default;
    public string description = default;

    private GameObject DescriptionUI;
    private Image baseImage;
    private GameObject outLineImage;

    private void Start()
    {
        DescriptionUI = GameObject.Find("Right_Item Description");
        outLineImage = transform.GetChild(0).gameObject;
        baseImage = transform.GetChild(1).GetComponent<Image>();
        baseImage.sprite = Resources.Load<Sprite>(icon);
    }

    //커서를 위에 올리면 실행되는 함수
    public void OnCursorPoint()
    {
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

    public void OnCursorPointUp()
    {
        outLineImage.SetActive(false);
    }
}
