using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : Character
{
    // 타겟
    private GameObject playerTarget;        // 공격 목표 캐릭터 , 김민섭_231013

    // 약점
    private GameObject weaknessPoint;       // 약점 위치, 김민섭_231013

    // 스킬
    [Header("스킬_메테오")]
    [SerializeField] private float meteor_currTime;          // 메테오 현재 남은 쿨타임, 김민섭_231013
    [SerializeField] private float meteor_coolTime;          // 메테오 쿨타임, 김민섭_231013

    // UI
    private UI_BossHUD ui_hud;                  // 보스 체력바, 김민섭_231013
    private UI_BossDistance ui_distance;        // 보스 거리, 김민섭_231013

    protected override IEnumerator Test_Delay()
    {
        if (weaknessPoint.activeSelf) yield break;

        Debug.Log("약점 노출!");
        weaknessPoint.SetActive(true);

        yield return new WaitForSeconds(3f);

        Debug.Log("약점 회복!");
        weaknessPoint.SetActive(false);
        State = CharacterState.IDLE;
    }

    /// <summary>
    /// 보스 초기화 함수
    /// 김민섭_231013
    /// </summary>
    protected override void Init()
    {
        movementSpeed = 3f;

        maxHp = 1000;
        currHp = maxHp;

        weaknessPoint = transform.Find("WeaknessPoint").gameObject;
        weaknessPoint.SetActive(false);

        meteor_coolTime = 2f;       // 5초다마 메테오 발동

        Debug.Log("보스 스탯 세팅 완료");

        // UI
        ui_hud = Managers.UI.ShowSceneUI<UI_BossHUD>();
        ui_hud.Init();
        ui_hud.SetTargetCharacter(this);
        ui_distance = Managers.UI.ShowSceneUI<UI_BossDistance>();
        ui_distance.Init();

        // TODO: 스킬 쿨타임 로직 실행
        StartCoroutine(Spell_Meteor());
        StartCoroutine(Spell_Summon());
    }

    /// <summary>
    /// 메테오 발동 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    private IEnumerator Spell_Meteor()
    {
        while(true)
        {
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
            float randY = Random.Range(transform.position.y + 10f, transform.position.y + 30f);

            Vector3 spawnPos = new Vector3(randX, randY, transform.position.z + 30f);
            Managers.Resource.Instantiate("Meteor", spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// 쫄몹 소환 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spell_Summon()
    {
        yield break;        // 임시, 김민섭_231013

        while(true)
        {
            if (State == CharacterState.DIE) yield break;       // 죽으면 정지

            // TODO: 몬스터 배열이 가득 차면 코루틴 임시 정지
            yield return new WaitUntil(() => true);
        }
    }

    protected override void Update()
    {
        base.Update();

        if(State != CharacterState.DIE && currHp <= 0)
        {
            State = CharacterState.DIE;
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            currHp -= 30;

            if(currHp % 10 == 0)
            {
                State = CharacterState.GROGGY;
                return;
            }
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
        currentTime += Time.deltaTime * movementSpeed;

        if(currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        // 움직임 함수
        transform.position = Vector3.Lerp(startPosition, endPosition, currentTime / lerpTime);
        
        // UI
        ui_distance.SetDistanceText(Vector3.Distance(transform.position, endPosition));
    }

    /// <summary>
    /// 보스가 SKILL 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateSkill()
    {
        if(currHp / maxHp <= 0.5f)
        {   // 50% 이하가 될 경우, 
            Debug.Log("2페이지 실행 중.");
        }
        else
        {   // 50%가 되지 않았을 경우,
            Debug.Log("1페이지 실행 중.");
        }
    }

    /// <summary>
    /// 보스가 DIE 상태일 때 실행되는 업데이트 함수
    /// 김민섭_231013
    /// </summary>
    protected override void UpdateDie()
    {
        Debug.Log("죽음");
    }
}
