using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class CameraManager
{
    public Transform inCastleCamera;
    public Transform outCastleCamera;
    private GameObject player;

    public float currTime = 0f;
    private float currMaxTime = 3f;

    public bool isGameStart;

    public void Init()
    {
        inCastleCamera = GameObject.Find("InCastleCamera").transform;
        outCastleCamera = GameObject.Find("OutCastleCamera").transform;
        player = GameObject.Find("Player");
    }

    public void OnUpdate()
    {
        if(player == null)
        {
            inCastleCamera = GameObject.Find("InCastleCamera").transform;
            outCastleCamera = GameObject.Find("OutCastleCamera").transform;
            player = GameObject.Find("Player");
        }

        if(player != null && isGameStart)
        {
            if(currTime < currMaxTime)
            {
                currTime += Time.deltaTime * 0.06f;

                player.transform.position = Vector3.Lerp(player.transform.position, outCastleCamera.position, currTime / currMaxTime);
            }
        }
    }

    public void StartCameraSet()
    {
        inCastleCamera.gameObject.SetActive(false);
    }
}
