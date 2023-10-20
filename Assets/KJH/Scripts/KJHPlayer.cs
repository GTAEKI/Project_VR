using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class KJHPlayer : MonoBehaviour
{
    public int money;
    public int currHp;

    public PCStatus status;
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGradingLayer;
    private Material yellowBorderMaterial; // 노란색 테두리를 그리기 위한 쉐이더를 담을 Material

    private void Start()
    {
        // Post-Processing Volume을 참조합니다.
        postProcessVolume = transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGradingLayer);

        //// 노란색 테두리 쉐이더를 로드하여 Material을 생성합니다.
        //Shader yellowBorderShader = Shader.Find("Custom/YellowBorder");
        //yellowBorderMaterial = new Material(yellowBorderShader);
    }

    public void PlayDamageEffect()
    {
        // 화면을 붉은색으로 번쩍이게 합니다.
        StartCoroutine(FlashScreen());
    }
    //public void PlayYellowBorderEffect()
    //{
    //    // 노란색 테두리 효과를 재생합니다.
    //    StartCoroutine(FlashYellowBorder());
    //}

    private IEnumerator FlashScreen()
    {
        // 화면을 붉은색으로 변경합니다.
        colorGradingLayer.colorFilter.value = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(0.05f); // 0.05초 동안 대기합니다.

        // 화면을 원래대로 돌립니다.
        colorGradingLayer.colorFilter.value = Color.white;
    }
    //private IEnumerator FlashYellowBorder()
    //{
    //    // 노란색 테두리를 화면의 사이드 부분에 표시합니다.
    //    // 노란색 효과를 활성화합니다.
    //    postProcessVolume.weight = 1f;

    //    // 화면에 노란색 테두리를 그립니다.
    //    Graphics.Blit(null, RenderTexture.active, yellowBorderMaterial);

    //    yield return new WaitForSeconds(0.05f); // 0.05초 동안 대기합니다.

    //    // 노란색 효과를 비활성화합니다.
    //    postProcessVolume.weight = 0f;
    //}
}