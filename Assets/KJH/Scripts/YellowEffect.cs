using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEffect : MonoBehaviour
{
    public Material yellowBorderMaterial; // 노란색 테두리를 그리기 위한 머티리얼

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, yellowBorderMaterial);
    }
}