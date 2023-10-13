using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    StoreObjectInfo remainStoreInfo;

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * 100000f, Color.red);

        if(Physics.Raycast(ray, out hitInfo,10000f))
        {
            if (hitInfo.collider.CompareTag("StoreObject"))
            {
                Debug.Log("일단 레이는 들어오는중");
                StoreObjectInfo storeInfo = hitInfo.collider.GetComponent<StoreObjectInfo>();
                remainStoreInfo = storeInfo;
                storeInfo.OnCursorPoint();
            }
            //else if(hitInfo.collider.CompareTag("BackgroundUI")) // 오류가 있음
            //{
            //    Debug.Log("else에 값 들어오는중");
            //    if(remainStoreInfo != null)
            //    {
            //        Debug.Log("remain에도 들어오는중");
            //        remainStoreInfo.OnCursorPointUp();
            //        remainStoreInfo = null;
            //    }
            //}
        }

        
        
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButton(0))
        {

        }
    }
}
