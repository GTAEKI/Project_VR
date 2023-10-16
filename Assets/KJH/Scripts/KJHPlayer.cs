using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class KJHPlayer : MonoBehaviour
{
    public int money;
    public int ad;
    public int testDamage = 50;
    public int currHp;
    public int maxHp = 20000;

    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGradingLayer;

    private void Start()
    {
        // Post-Processing Volume을 참조합니다.
        postProcessVolume = transform.Find("Main Camera").GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGradingLayer);

    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 누를 때의 입력을 감지합니다.
        if (Input.GetMouseButtonDown(0))
        {
                // 화면을 붉은색으로 번쩍이게 합니다.
                StartCoroutine(FlashScreen());

                TakeDamage();
        }
    }

    private IEnumerator FlashScreen()
    {
        // 화면을 붉은색으로 변경합니다.
        colorGradingLayer.colorFilter.value = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(0.05f); // 0.05초 동안 대기합니다.

        // 화면을 원래대로 돌립니다.
        colorGradingLayer.colorFilter.value = Color.white;
    }

    private void TakeDamage()
    {
        currHp -= testDamage;

        Debug.Log(currHp);
    }

    private void Die()
    {

    }
}