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

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * 100000f, Color.red);

        if(Physics.Raycast(ray, out hitInfo,10000f,LayerMask.GetMask("Store")))
        {
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.collider.CompareTag("StoreObject"))
            {
                Debug.Log("일단 레이는 들어오는중");
                StoreObjectInfo storeInfo = hitInfo.collider.GetComponent<StoreObjectInfo>();
                remainStoreInfo = storeInfo;
                storeInfo.OnCursorPoint();

                //if (Input.GetMouseButtonDown(0))
                //{
                //    storeInfo.BuyUnit();
                //}
                //else if (Input.GetMouseButton(0))
                //{
                    
                //}
                //else if (Input.GetMouseButtonUp(0))
                //{
                //    storeInfo.DontBuyUnit();
                //}

                if(ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
                {
                    storeInfo.BuyUnit();
                }
                else if(ARAVRInput.GetUp(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
                {
                    storeInfo.DontBuyUnit();
                }

            }
            else if (hitInfo.collider.CompareTag("BackgroundUI")) // 오류가 있음
            {
                StoreObjectInfo.OtherOutLineOff(); // 백그라운드 UI를 건드리면 다른 아웃라인은 전부 끔
            }
        }

        
        
    }
}
