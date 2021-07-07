using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameplateScript : MonoBehaviour
{
    [SerializeField] private TMP_Text nameplate;
    [SerializeField] private PlayerScript charScript;

    private void OnEnable()
    {
        charScript.OnDataUpdate += OnUpdate;
    }

    private void OnDisable()
    {
        charScript.OnDataUpdate -= OnUpdate;
    }

    private void OnUpdate(PlayerData data)
    {
        nameplate.text = data.playerName;
    }
}
