using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class CameraManager
{
    private CinemachineVirtualCamera inCastleCamera;
    private CinemachineVirtualCamera outCastleCamera;

    public void Init()
    {
        inCastleCamera = GameObject.Find("InCastleCamera").GetComponent<CinemachineVirtualCamera>();
        outCastleCamera = GameObject.Find("OutCastleCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public void StartCameraSet()
    {
        inCastleCamera.gameObject.SetActive(false);
    }
}
