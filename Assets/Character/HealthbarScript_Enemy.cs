using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript_Enemy : MonoBehaviour
{
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float lerpSpeed;

    private float lerpVal = 0f;
    private float oldVal, newVal;

    private void Start()
    {
        SetValue(enemyScript.GetCurrentHealth());
    }

    private void Update()
    {
        if(lerpVal < 1f)
        {
            lerpVal = Mathf.Clamp01(lerpVal + (Time.deltaTime * lerpSpeed));
            var value = Mathf.Lerp(oldVal, newVal, lerpVal);
            SetValue(value);
        }
    }

    private void SetValue(float value)
    {
        healthSlider.value = value / enemyScript.GetMaxHealth();
    }

    private void OnEnable()
    {
        enemyScript.OnHealthUpdate += OnHealthUpdate;
    }

    private void OnDisable()
    {
        enemyScript.OnHealthUpdate -= OnHealthUpdate;
    }

    private void OnHealthUpdate(float oldval, float newval)
    {
        this.oldVal = oldval;
        this.newVal = newval;
        lerpVal = 0f;
    }
}
