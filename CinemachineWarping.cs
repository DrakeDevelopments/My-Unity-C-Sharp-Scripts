using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineWarping : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void CameraWarp()
    {
        cinemachineVirtualCamera.OnTargetObjectWarped(cinemachineVirtualCamera.Follow, cinemachineVirtualCamera.Follow.position);
        Debug.Log(cinemachineVirtualCamera.Follow);
    }
}
