using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private GameObject bigHitPrefab;
    [SerializeField] private GameObject smallHitPrefab;
    [SerializeField] private GameObject bloodSplatPrefab;

    public void SpawnFloatingText(string text, Color color, Vector3 worldPosition, float fontSize = 16, float floatDistance = 1, float duration = 1f)
    {
        if (CameraManager.Instance.IsPositionOutOfCamera(worldPosition)) return;

        var floatingText = Instantiate(floatingTextPrefab, worldPosition, Quaternion.identity, transform).GetComponent<FloatingText>();

        floatingText.Initialize(text, color, fontSize, floatDistance, duration);
    }

    public void SpawnHit(Vector3 worldPosition, bool isSmall) => Instantiate(isSmall? smallHitPrefab : bigHitPrefab, worldPosition, Quaternion.identity);

    public void SpawnBloodSplat(Vector3 worldPosition) => Instantiate(bloodSplatPrefab, worldPosition.normalized, Quaternion.identity);
}
