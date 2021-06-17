using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private GameObject floatingTextPrefab;

    public void SpawnFloatingText(string text, Color color, Vector3 worldPosition, float fontSize = 16, float floatDistance = 1, float duration = 1f)
    {
        if (CameraManager.Instance.IsPositionOutOfCamera(worldPosition)) return;

        var floatingText = Instantiate(floatingTextPrefab, worldPosition, Quaternion.identity, transform).GetComponent<FloatingText>();

        floatingText.Initialize(text, color, fontSize, floatDistance, duration);
    }
}
