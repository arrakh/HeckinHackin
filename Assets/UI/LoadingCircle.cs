using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    private RectTransform rectComponent;

    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float fillSpeed = 2f;

    [SerializeField] private Image progress;

    private float temp = 0f;

    private void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        temp += fillSpeed * Time.deltaTime;
        progress.fillAmount = (Mathf.Sin(temp) + 1) / 2;
    }
}
