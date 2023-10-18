using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public Transform target; // 추적할 대상의 Transform (적 캐릭터 등)
    public float rotationSpeed = 10f; // 포탑 회전 속도
    public float attackRange = 10f; // 포탑이 공격 가능한 최대 거리
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치
    public float fireRate = 1f; // 공격 속도 (1초에 발사하는 횟수)

    private float fireCountdown = 0f;

    void Update()
    {
        // 대상이 없거나 대상이 포탑의 사정거리를 벗어나면 반환
        if (target == null || Vector3.Distance(transform.position, target.position) > attackRange)
        {
            return;
        }

        // 대상의 방향으로 포탑을 부드럽게 회전시킴
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // 일정 간격으로 자동 공격
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // 총알 발사 로직
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target); // 총알이 대상을 추적하도록 Seek 메서드 호출
        }
    }
}