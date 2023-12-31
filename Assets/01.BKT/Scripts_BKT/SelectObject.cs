using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 마우스 선택을 위해 만든 클래스, 오큘러스 연동시 변경이 필요함
/// 배경택
/// </summary>
public class SelectObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    StoreObjectInfo remainStoreInfo;
    StartButton closeStartButtonOutLine;
    EndButton closeEndButtonOutLine;

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * 100000f, Color.red);

        if(Physics.Raycast(ray, out hitInfo,10000f,LayerMask.GetMask("UI")))
        {
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.collider.CompareTag("StoreObject"))
            {
                Debug.Log("일단 레이는 들어오는중");
                StoreObjectInfo storeInfo = hitInfo.collider.GetComponent<StoreObjectInfo>();
                remainStoreInfo = storeInfo;
                storeInfo.OnCursorPoint();

                if(ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
                {
                    storeInfo.BuyUnit();
                }
                else if(ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
                {
                    storeInfo.DontBuyUnit();
                }

            }
            else if (hitInfo.collider.CompareTag("BackgroundUI")) // 오류가 있음
            {
                StoreObjectInfo.OtherOutLineOff(); // 백그라운드 UI를 건드리면 다른 아웃라인은 전부 끔
            }

            

            if (hitInfo.collider.CompareTag("StartButton"))
            {
                Debug.Log("StartButton");
                StartButton startButton = hitInfo.collider.GetComponent<StartButton>();
                startButton.transform.GetChild(1).gameObject.SetActive(true);
                closeStartButtonOutLine = startButton;
                if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
                {
                    if (Managers.GameManager.isGameOver == true)
                    {
                        startButton.ClickReStartButton();
                    }
                    else
                    {
                        startButton.ClickStartButton();
                    }
                }
                else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
                {
                    
                }
            }
            

            if (hitInfo.collider.CompareTag("EndButton"))
            {
                Debug.Log("EndButton");
                EndButton endButton = hitInfo.collider.GetComponent<EndButton>();
                endButton.transform.GetChild(1).gameObject.SetActive(true);
                closeEndButtonOutLine = endButton;

                if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
                {
                    endButton.ClickEndGame();
                }
                else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
                {

                }
            }
        }
        else
        {
            if(closeStartButtonOutLine!= null)
            {
                closeStartButtonOutLine.transform.GetChild(1).gameObject.SetActive(false);

            }

            if(closeEndButtonOutLine != null)
            {
                closeEndButtonOutLine.transform.GetChild(1).gameObject.SetActive(false);

            }
        }



    }
}
