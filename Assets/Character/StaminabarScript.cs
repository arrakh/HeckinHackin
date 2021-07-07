using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminabarScript : MonoBehaviour
{
    [SerializeField] private PlayerScript charScript;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private float lerpSpeed;

    private float lerpVal = 0f;
    private float oldVal, newVal;

    private void Start()
    {
        SetValue(charScript.GetCurrentStamina());
    }

    private void Update()
    {
        if (lerpVal < 1f)
        {
            lerpVal = Mathf.Clamp01(lerpVal + (Time.deltaTime * lerpSpeed));
            var value = Mathf.Lerp(oldVal, newVal, lerpVal);
            SetValue(value);
        }
    }

    private void SetValue(float value)
    {
        staminaSlider.value = value / charScript.GetMaxStamina();
        staminaText.text = Mathf.RoundToInt(value).ToString();
    }

    private void OnEnable()
    {
        charScript.OnStaminaUpdate += OnUpdate;
    }

    private void OnDisable()
    {
        charScript.OnStaminaUpdate -= OnUpdate;
    }

    private void OnUpdate(float oldval, float newval)
    {
        this.oldVal = oldval;
        this.newVal = newval;
        lerpVal = 0f;
    }
}
