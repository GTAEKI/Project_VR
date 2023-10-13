using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// UnityEngine.Rendering.PostProcessing;

public class Aim : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public GameObject bulletPrefab;
    // 어떤 손인지 판단
    public bool isLeftHand = default;

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
        // 사용자가 indexTrigger 버튼을 누르면
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            // 컨트롤러의 진동 재생
            ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch);

            Shoot();
            // Ray를 카메라의 위치로부터 나가도록 만든다.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            // Ray의 충돌 정보를 저장하기 위한 변수 지정
            RaycastHit hitInfo;

        }
        if (isLeftHand)
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
        }       // if : 왼쪽 핸드 기준으로 레이저 포인터 만들기

        else
        {
            // 오른쪽 컨트롤러 기준으로 Ray를 만든다.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
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
                lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * lrMaxDistance);
            }
        }       // else : 오른쪽 핸드 기준으로 레이저 포인터 만들기

    }
    void Shoot()
    {// Ray를 카메라의 위치로부터 나가도록 만든다.
        Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        // Ray의 충돌 정보를 저장하기 위한 변수 지정
        RaycastHit hitInfo;
        // 총알 프리팹 인스턴스화 후 총구 위치에서 발사
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // 총알의 초기 속도와 방향 설정
        bulletRb.velocity = ray.direction * bulletSpeed; // bulletSpeed는 총알의 원하는 속도

        // 총알 발사 후 일정 시간이 지나면 파괴
        Destroy(bullet, bulletLifetime); // bulletLifetime은 총알이 파괴될 시간 (초 단위)
    }
}