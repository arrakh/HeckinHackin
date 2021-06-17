using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera currentCam;
    [SerializeField] private Camera unityCamera;

    public void SetFollow(GameObject toFollow)
    {
        currentCam.Follow = toFollow.transform;
    }

    public Camera GetCamera() => unityCamera;

    public bool IsPositionOutOfCamera(Vector3 position)
    {
        var val = unityCamera.WorldToViewportPoint(position);
        return val.x > 1 || val.x < 0 || val.y > 1 || val.y < 0;
    }
}
