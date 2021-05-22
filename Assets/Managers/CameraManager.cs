using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera currentCam;

    public void SetFollow(GameObject toFollow)
    {
        currentCam.Follow = toFollow.transform;
    }
}
