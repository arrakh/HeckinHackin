using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private TMPro.TMP_Text pointText;

    public void Initialize(string name, string point)
    {
        nameText.text = name;
        pointText.text = point;
    }
}
