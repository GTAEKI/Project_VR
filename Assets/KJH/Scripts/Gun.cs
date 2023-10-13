using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    
    void Start()
    {

    }

    void Update()
    {
        // 사용자가 indexTrigger 버튼을 누르면
        if(ARAVRInput.GetDown(ARAVRInput.Button. IndexTrigger))
        {
            // 컨트롤러의 진동 재생
            ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch);

            Shoot();
            // Ray를 카메라의 위치로부터 나가도록 만든다.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            // Ray의 충돌 정보를 저장하기 위한 변수 지정
            RaycastHit hitInfo;
           
        }

    }

    void Shoot()
    {
        // 총알 프리팹 인스턴스화 후 총구 위치에서 발사
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // 총알에 앞으로 가는 힘을 추가하여 발사합니다.
        bulletRb.AddForce(transform.forward * bulletRb.mass * 10f, ForceMode.Impulse);
    }
}
