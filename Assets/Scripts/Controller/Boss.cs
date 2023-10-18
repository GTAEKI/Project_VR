using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Boss : Character
{
    // 타겟
    private GameObject playerTarget;        // 공격 목표 캐릭터 , 김민섭_231013

    // 약점
    private GameObject weaknessPoint;       // 약점 위치, 김민섭_231013

    // 스킬, 김민섭_231013
    [Header("TEST: 스킬_메테오")]
    [SerializeField]
    [Tooltip("메테오 현재 남은 쿨타임")] private float meteor_currTime;
    [SerializeField] 
    [Tooltip("메테오 쿨타임")] private float meteor_coolTime;            
    private bool isSummon = false;

    // 스탯
    [Header("TEST: 골렘 스탯")]
    [SerializeField] private GolemStatus currStatus;             // 보스 스탯, 김민섭_231014
    [Header("TEST: 스폰 데이터 스탯")]
    [SerializeField] private MinionSpawn spawnStatus;            // 졸개 소환 스탯, 김민섭_231015

    // UI
    private UI_BossHUD ui_hud;                  // 보스 체력바, 김민섭_231013
    private UI_BossDistance ui_distance;        // 보스 거리, 김민섭_231013

    /// <summary>
    /// 보스 현재 스탯 Get 프로퍼티
    /// 김민섭_231014
    /// </summary>
    public GolemStatus CurrStatus => currStatus; 

    protected override IEnumerator Test_Delay()
    {
        if (weaknessPoint.activeSelf) yield break;

        Debug.Log("약점 노출!");
        weaknessPoint.SetActive(true);

        yield return new WaitForSeconds(currStatus.ActTime);

        Debug.Log("약점 회복!");
        currStatus.IsGroggy = false;
        weaknessPoint.SetActive(false);
        State = CharacterState.IDLE;
    }

    /// <summary>
    /// 보스 초기화 함수
    /// 김민섭_231013
    /// </summary>
    protected override void Init()
    {
        weaknessPoint = transform.Find("RockCreatureMesh_LP/WeaknessPoint").gameObject;
        weaknessPoint.SetActive(false);

        meteor_coolTime = 8f;       // 5초다마 메테오 발동

        // 보스 스탯 세팅 진행
        currStatus = new GolemStatus(Define.Data_ID_List.Golem);
        maxHp = currStatus.Hp;
        spawnStatus = new MinionSpawn(Define.Data_ID_List.Spawn_Phase1);

        // UI
        ui_hud = Managers.UI.ShowSceneUI<UI_BossHUD>();
        ui_hud.Init();
        ui_hud.SetTargetCharacter(this);
        ui_distance = Managers.UI.ShowSceneUI<UI_BossDistance>();
        ui_distance.Init();

        // 스킬 로직 실행
        StartCoroutine(Spell_Meteor());
        StartCoroutine(Spell_Summon());

        StartCoroutine(DelayIDLE());
    }

    /// TODO: 나중에 버튼 누르면 재생 되도록 제어 할 것, 김민섭_231017
    private IEnumerator DelayIDLE()
    {
        yield return new WaitForSeconds(1f);

        State = CharacterState.RUBBLE_TO_IDLE;
    }

    /// <summary>
    /// 메테오 발동 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    private IEnumerator Spell_Meteor()
    {
        while(true)
        {
            yield return new WaitUntil(() => State != CharacterState.RUBBLE && State != CharacterState.RUBBLE_TO_IDLE);
            if (State == CharacterState.DIE) yield break;       // 죽으면 정지

            meteor_currTime += Time.deltaTime;

            if(meteor_currTime >= meteor_coolTime)
            {   // 쿨타임에 도달하면 공격
                Debug.Log("메테오 발동!");
                Meteor();

                meteor_currTime = 0f;

                // 메테오 모두 발동 후 상태 변환
                if(weaknessPoint.activeSelf)
                {
                    State = CharacterState.GROGGY;
                }
                else
                {
                    State = CharacterState.IDLE;
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// 메테오 발동 함수
    /// 김민섭_231013
    /// </summary>
    private void Meteor()
    {
        // 운석 개수 결정
        int spawnCount = Random.Range(5, 10);

        for(int i = 0; i < spawnCount; i++)
        {
            // 스폰 위치 결정
            // TODO: 현재 보스의 위치에서 약간 후방에 생성
            float randX = Random.Range(transform.position.x - 50f, transform.position.x + 50f);
            float randY = Random.Range(transform.position.y + 30f, transform.position.y + 60f);

            Vector3 spawnPos = new Vector3(randX, randY, transform.position.z + 30f);
            Managers.Resource.Instantiate("Meteor", spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// 졸개 소환 코루틴 함수
    /// 김민섭_231015
    /// </summary>
    /// <param name="type">졸개 타입</param>
    /// <param name="amount">생성되는 수량</param>
    private IEnumerator SpawnMinion(int type, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (State == CharacterState.GROGGY)
            {
                isSummon = false;
                yield break;
            }
            
            float randX = Random.Range(transform.position.x - 50f, transform.position.x + 50f);
            Vector3 spawnPos = new Vector3(randX, 1.5f, transform.position.z - 30f);

            GameObject spawnMinion = Managers.Resource.Instantiate("Minion", transform.position, Quaternion.identity);

            float currentTime = 0f;
            float totalTime = 1f; // 총 소환 시간 (조절 가능)
            Vector3 startPos = weaknessPoint.transform.position;
            Vector3 initialVelocity = CalculateInitialVelocity(spawnPos, startPos, totalTime);

            while (currentTime < totalTime)
            {
                float t = currentTime / totalTime;
                Vector3 position = CalculateParabolicTrajectory(startPos, initialVelocity, t);
                spawnMinion.transform.position = position;

                currentTime += Time.deltaTime;
                yield return null;
            }

            Debug.Log("소환 완료");

            if (type == (int)Define.Data_ID_List.Minion_Fast)
            {
                spawnMinion.AddComponent<FastMinionController>();
            }
            else
            {
                spawnMinion.AddComponent<PowerMinionController>();
            }
        }

        if (amount > 0)
        {
            isSummon = false;
        }
    }

    // 초기 속도 계산 함수
    private Vector3 CalculateInitialVelocity(Vector3 target, Vector3 start, float time)
    {
        Vector3 displacement = target - start;
        Vector3 velocity = displacement / time - 0.5f * Physics.gravity * time;
        return velocity;
    }

    // 포물선 운동 계산 함수
    private Vector3 CalculateParabolicTrajectory(Vector3 start, Vector3 initialVelocity, float time)
    {
        Vector3 position = start + initialVelocity * time + 0.5f * Physics.gravity * time * time;
        return position;
    }

    /// <summary>
    /// 졸개 소환 함수
    /// 김민섭_231015
    /// </summary>
    private void Summon()
    {
        StartCoroutine(SpawnMinion((int)Define.Data_ID_List.Minion_Fast, spawnStatus.Type1_Amount));
        StartCoroutine(SpawnMinion((int)Define.Data_ID_List.Minion_Power, spawnStatus.Type2_Amount));
    }

    /// <summary>
    /// 쫄몹 소환 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spell_Summon()
    {
        while(true)
        {
            yield return new WaitUntil(() => State != CharacterState.RUBBLE && State != CharacterState.RUBBLE_TO_IDLE);
            yield return new WaitUntil(() => !isSummon);

            if (State == CharacterState.DIE) yield break;       // 죽으면 정지

            // TODO: 몬스터 배열이 가득 차면 코루틴 임시 정지
            if (playerTarget != null)
            {
                float distance = Vector3.Distance(transform.position, playerTarget.transform.position);
                if ((int)distance % 12 == 0)
                {
                    Debug.Log("졸개 소환!");

                    isSummon = true;
                    Summon();

                    // 메테오 모두 발동 후 상태 변환
                    if (weaknessPoint.activeSelf)
                    {
                        State = CharacterState.GROGGY;
                    }
                    else
                    {
                        State = CharacterState.IDLE;
                    }

                    // TODO: 소환된 졸개가 없다면 실행되게 수정
                    //yield return new WaitForSeconds(spawnStatus.Spawn_Time);

                    //isSummon = false;
                }
            }
            yield return null;
        }
    }

    protected override void Update()
    {
        if(currStatus != null && currStatus.IsDie)
        {   // 현재 죽은 상태라면 행동 정지
            if(State != CharacterState.DIE) State = CharacterState.DIE;
            return;
        }

        if(State != CharacterState.GROGGY && currStatus.IsGroggy)
        {
            State = CharacterState.GROGGY;
            return;
        }

        base.Update();

        if(Input.GetMouseButtonDown(0))
        {
            currStatus.OnDamaged(10);
        }
    }

    /// <summary>
    /// 보스가 IDLE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateIdle()
    {
        if(playerTarget != null)
        {   // 타겟이 있다면 움직임 시작
            State = CharacterState.MOVE;
            return;
        }

        if(playerTarget == null)
        {   // 타겟이 없다면 탐색 시작
            Search();
        }
    }

    /// <summary>
    /// 타겟을 탐색하는 함수
    /// 김민섭_231013
    /// </summary>
    private void Search()
    {
        Collider[] findTarget = Physics.OverlapSphere(transform.position, 1000f, LayerMask.GetMask("Player"));
        if (findTarget.Length <= 0)
        {
            Debug.LogWarning("타겟{플레이어}를 찾을 수 없습니다.");
            return;
        }

        // 타겟을 찾았으면 움직임 상태로 전환
        playerTarget = findTarget.First().gameObject;
        startPosition = transform.position;
        endPosition = playerTarget.transform.position;
        endPosition.y = 7.5f;       // 임시 코드 , 김민섭_231013
        State = CharacterState.MOVE;
        return;
    }

    /// <summary>
    /// 보스가 MOVE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateMove()
    {
        if(playerTarget == null)
        {
            Debug.LogWarning("타겟{플레이어}를 찾을 수 없습니다.");
            State = CharacterState.IDLE;
            return;
        }

        // 타겟이 있다면 타겟을 향해 이동
        currentTime += Time.deltaTime * currStatus.MoveSpeed;

        if(currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        // 움직임 함수
        transform.position = Vector3.Lerp(startPosition, endPosition, currentTime / lerpTime);
        
        // UI
        ui_distance.SetDistanceText(Vector3.Distance(transform.position, endPosition));
    }
}
