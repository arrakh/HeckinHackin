using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(RectTransform))]
public class FloatingText : MonoBehaviour
{
    private TMP_Text numberText;
    private RectTransform rect;

    public void Initialize(string text, Color color, float fontSize, float floatDistance, float duration)
    {
        rect = GetComponent<RectTransform>();
        transform.DOLocalMoveY(transform.localPosition.y + floatDistance, duration)
            .SetEase(Ease.OutQuart);

        numberText = GetComponent<TMP_Text>();
        numberText.text = text;
        numberText.color = color;
        numberText.fontSize = fontSize;
        numberText.DOFade(0, duration)
            .SetEase(Ease.OutQuart)
            .OnComplete(delegate { Destroy(this.gameObject); });
    }

}
