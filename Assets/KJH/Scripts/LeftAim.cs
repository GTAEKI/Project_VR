using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeftAim : MonoBehaviour
{
    // 레이저 포인트를 발사할 라인 렌더러
    LineRenderer lineRenderer;



    // 레이저 포인터의 최대 거리
    [SerializeField]
    private float lrMaxDistance = 200f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // 왼쪽 컨트롤러 기준으로 Ray를 만든다.
        Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
        RaycastHit hitInfo;

        // 충돌이 있다면?
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Ray가 부딪힌 지점에 라인 그리기
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        // 충돌이 없다면?
        else
        {
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * lrMaxDistance);
        }
    }
}