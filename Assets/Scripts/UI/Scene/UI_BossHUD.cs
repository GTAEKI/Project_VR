using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHUD : UI_Scene
{
    private enum Images
    {
        Img_Shield,
        Img_Damaged,
        Img_Hp,
        Img_Separator,
       // Img_Mana
    }

    private Camera uiCamera;
    private Boss boss;

    private static readonly int floatSteps = Shader.PropertyToID(STEP);
    private static readonly int floatRatio = Shader.PropertyToID(RATIO);
    private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
    private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);

    private float hpShieldRatio;        // HP Shield
    private float rectWidth = 100f;
    private float thickness = 0.5f;

    #region 쉐이더 프로퍼티

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";
    private const float HP_RATIO = 100f;

    #endregion

    #region 쉴드 테스트 변수

    private float sp = 0f;      // 쉴드
    private float speed = 3f;

    #endregion

    /// <summary>
    /// 초기화 함수
    /// 김민섭_231013
    /// </summary>
    public override void Init()
    {
        Bind<Image>(typeof(Images));

        uiCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        GetComponent<Canvas>().worldCamera = uiCamera;

        CreateMaterial();
    }

    /// <summary>
    /// HUD의 머티리얼을 생성합니다.
    /// 김민섭_231013
    /// </summary>
    private void CreateMaterial()
    {
        GetImage((int)Images.Img_Separator).material = new Material(Shader.Find("MinSeob/UI/HUD"));
    }

    /// <summary>
    /// UI의 타겟을 지정하는 함수
    /// 김민섭_231013
    /// </summary>
    /// <param name="golem"></param>
    public void SetTargetCharacter(Boss golem)
    {
        this.boss = golem;

        StartCoroutine(RefreshHUDValue());
    }

    /// <summary>
    /// 체력바 값 제어 코루틴 함수
    /// 김민섭_231013
    /// </summary>
    private IEnumerator RefreshHUDValue()
    {
        while(true)
        {
            float step;

            if(sp > 0)
            {   // 쉴드가 있을 경우
                if(boss.CurrStatus.Hp + sp > boss.MaxHp)
                {   // 현재 체력 + 쉴드량이 최대 체력보다 높다면
                    float value = (float)(boss.CurrStatus.Hp / (boss.CurrStatus.Hp + sp));
                    hpShieldRatio = value;
                    step = boss.CurrStatus.Hp / HP_RATIO;
                    GetImage((int)Images.Img_Hp).fillAmount = value;
                }
                else
                {   // 현재 체력 + 쉴드량이 최대 체력보다 낮다면
                    float value = (float)boss.CurrStatus.Hp / boss.MaxHp;
                    hpShieldRatio = value;
                    step = boss.CurrStatus.Hp / hpShieldRatio;
                    GetImage((int)Images.Img_Hp).fillAmount = value;
                }
            }
            else
            {   // 쉴드가 없다면
                step = boss.MaxHp / HP_RATIO;
                hpShieldRatio = 1f;
                GetImage((int)Images.Img_Hp).fillAmount = (float)boss.CurrStatus.Hp / boss.MaxHp;
            }

            GetImage((int)Images.Img_Damaged).fillAmount = Mathf.Lerp(GetImage((int)Images.Img_Damaged).fillAmount, GetImage((int)Images.Img_Hp).fillAmount, Time.deltaTime * speed);
            GetImage((int)Images.Img_Separator).material.SetFloat(floatSteps, step); // floatSteps처럼 float 변수에 쉐이더 값을 조절하는 변수
            GetImage((int)Images.Img_Separator).material.SetFloat(floatRatio, hpShieldRatio); // int가 Enum 앞에 있으면 index 번호대로 들어감
            GetImage((int)Images.Img_Separator).material.SetFloat(floatWidth, rectWidth);
            GetImage((int)Images.Img_Separator).material.SetFloat(floatThickness, thickness);

            yield return null;
        }
    }

    #region Coroutine Test

    //private IEnumerator CoroutineTest()
    //{
    //    yield return new WaitForSeconds(2f);

    //    // hp = 1500;
    //    // maxHp = 1500;
    //    sp = 400;

    //    while (sp > 0)
    //    {
    //        sp -= (int)(280 * Time.deltaTime);
    //        yield return null;
    //    }

    //    sp = 0;

    //    yield return new WaitForSeconds(2f);

    //    for (int i = 0; i < 8; i++)
    //    {
    //        unit.CurrentUnitStat.OnDamaged(120f);
    //        yield return new WaitForSeconds(1f);
    //    }

    //    for (int i = 0; i < 8; i++)
    //    {
    //        unit.CurrentUnitStat.UnitStat.OnChangeMaxHp(200f);
    //        unit.CurrentUnitStat.SettingHp(unit.CurrentUnitStat.UnitStat.Hp);

    //        yield return new WaitForSeconds(1f);
    //    }

    //    // UnityEditor.EditorApplication.isPlaying = false;
    //}

    #endregion
}
