using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField] private PlayerScript charScript;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private float lerpSpeed;

    private float lerpVal = 0f;
    private float oldVal, newVal;

    private void Start()
    {
        SetValue(charScript.GetCurrentHealth());
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
        healthSlider.value = value / charScript.GetMaxHealth();
        healthText.text = Mathf.RoundToInt(value).ToString();
    }

    private void OnEnable()
    {
        charScript.OnHealthUpdate += OnHealthUpdate;
    }

    private void OnDisable()
    {
        charScript.OnHealthUpdate -= OnHealthUpdate;
    }

    private void OnHealthUpdate(float oldval, float newval)
    {
        this.oldVal = oldval;
        this.newVal = newval;
        lerpVal = 0f;
    }
}
